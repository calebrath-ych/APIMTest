using System.Reflection;
using System.Xml.Linq;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ych;

namespace Ych.Apim;

public class ApimDefinitionManager
{
    public string DefinitionsFolder => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        @"../../../../../../", "APIv3/apiv3/Ych.Apim/definitions"));

    public string ApiFolderPath => Path.Combine(DefinitionsFolder, "apis");
    public string SubscriptionsFolderPath => Path.Combine(DefinitionsFolder, "subscriptions");

    public class EndpointDetail
    {
        public string FunctionName { get; set; }
        public string Method { get; set; }
        public string Route { get; set; }
        public List<string> Parameters { get; set; }

        public override string ToString() => $"{Route} ({Method.ToUpper()})";
    }

    public IEnumerable<string?> ListApis()
    {
        return Directory.GetDirectories(ApiFolderPath).Select(Path.GetFileName);
    }

    public IEnumerable<string?> ListSubscriptions()
    {
        return Directory.GetDirectories(SubscriptionsFolderPath).Select(Path.GetFileName);
    }

    public IEnumerable<EndpointDetail> ListEndpoints()
    {
        List<EndpointDetail> endpoints = new List<EndpointDetail>();

        Assembly assembly = Assembly.Load("YchApiFunctions");
        MethodInfo[] functions = assembly.GetTypes()
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(typeof(FunctionAttribute), false).Any())
            .ToArray();

        foreach (var function in functions)
        {
            FunctionAttribute? functionAttribute = function.GetCustomAttribute<FunctionAttribute>();
            HttpTriggerAttribute? httpTriggerAttribute = function.GetParameters()
                .SelectMany(p => p.GetCustomAttributes(typeof(HttpTriggerAttribute), false))
                .Cast<HttpTriggerAttribute>()
                .FirstOrDefault();

            if (functionAttribute == null || httpTriggerAttribute == null)
            {
                continue;
            }

            string functionName = functionAttribute.Name;
            string method = httpTriggerAttribute.Methods.FirstOrDefault()?.ToLower();
            string route = httpTriggerAttribute.Route;
            List<string> parameters = function.GetParameters()
                .Where(p => route.Contains($"{{{p.Name}}}"))
                .Select(p => p.Name)
                .ToList();

            if (!string.IsNullOrEmpty(functionName) && !string.IsNullOrEmpty(method) && !string.IsNullOrEmpty(route))
            {
                endpoints.Add(new EndpointDetail
                {
                    FunctionName = functionName,
                    Method = method,
                    Route = route,
                    Parameters = parameters
                });
            }
        }

        return endpoints;
    }

    public JObject CreatePathObject(EndpointDetail endpointDetail)
    {
        return new JObject
        {
            [endpointDetail.Method] = new JObject
            {
                ["summary"] = endpointDetail.FunctionName,
                ["operationId"] = $"{endpointDetail.Method}-{endpointDetail.FunctionName.ToLower()}",
                ["parameters"] = new JArray(
                    endpointDetail.Parameters.Select(param => new JObject
                    {
                        ["name"] = param,
                        ["in"] = "path",
                        ["required"] = true,
                        ["schema"] = new JObject { ["type"] = "string" }
                    })
                ),
                ["responses"] = new JObject
                {
                    ["200"] = new JObject { ["description"] = "Success" }
                }
            }
        };
    }

    public void CreateApi(string displayName, string path, IEnumerable<string> endpoints,
        string createdFrom = $"Created from {nameof(ApimDefinitionManager)}.")
    {
        string apiName = displayName.Replace(" ", "").PascalToKebabCase().ToLower();
        string specificApiFolder = Path.Combine(ApiFolderPath, apiName);

        Directory.CreateDirectory(specificApiFolder);

        string apiInformationFile = Path.Combine(specificApiFolder, "apiInformation.json");

        var apiInformation = new
        {
            properties = new
            {
                path = path,
                apiRevision = "1",
                authenticationSettings = new { },
                description = createdFrom,
                isCurrent = true,
                displayName = displayName,
                protocols = new[] { "https" },
                subscriptionKeyParameterNames = new
                {
                    header = "Ocp-Apim-Subscription-Key",
                    query = "subscription-key"
                },
                subscriptionRequired = true
            }
        };

        // Serialize the extended JSON content and save to file
        File.WriteAllText(apiInformationFile, JsonConvert.SerializeObject(apiInformation, Formatting.Indented));
        
        string specificationFile = Path.Combine(specificApiFolder, "specification.json");

        var specification = new JObject
        {
            ["openapi"] = "3.0.1",
            ["info"] = new JObject
            {
                ["title"] = displayName,
                ["description"] = $"Created from YchConsole:{nameof(CreateApi)} command.",
                ["version"] = "1.0"
            },
            ["paths"] = new JObject(),
            ["components"] = new JObject
            {
                ["securitySchemes"] = new JObject
                {
                    ["apiKeyHeader"] = new JObject
                    {
                        ["type"] = "apiKey",
                        ["name"] = "Ocp-Apim-Subscription-Key",
                        ["in"] = "header"
                    },
                    ["apiKeyQuery"] = new JObject
                    {
                        ["type"] = "apiKey",
                        ["name"] = "subscription-key",
                        ["in"] = "query"
                    }
                }
            },
            ["security"] = new JArray
            {
                new JObject { ["apiKeyHeader"] = new JArray() },
                new JObject { ["apiKeyQuery"] = new JArray() }
            }
        };
        
        File.WriteAllText(specificationFile, JsonConvert.SerializeObject(apiInformation, Formatting.Indented));

        string operationsFolder = Path.Combine(specificApiFolder, "operations");

        Directory.CreateDirectory(operationsFolder);

        var endpointObjects = ListEndpoints().Where(s => endpoints.Contains(s.FunctionName));

        AddOrUpdateEndpointsToApis([apiName], endpoints);

        var apiFilePath = Path.Combine(specificApiFolder, "specification.json");
        File.WriteAllText(apiFilePath, JsonConvert.SerializeObject(specification, Formatting.Indented));
    }

    public void CreateSubscription(string displayName, string scope)
    {
        var subscriptionData = new
        {
            properties = new
            {
                displayName = displayName,
                scope =
                    $"/subscriptions/{{{{subscription-guid}}}}/resourceGroups/{{{{resource-name}}}}/providers/Microsoft.ApiManagement/service/{{{{apim-instance-name}}}}/apis/{scope}",
                allowTracing = false,
                state = "active"
            }
        };

        var newSubscriptionFolderPath = Path.Combine(SubscriptionsFolderPath, displayName.Replace(" ", "").PascalToKebabCase());

        Directory.CreateDirectory(newSubscriptionFolderPath);

        var subscriptionJsonFilePath = Path.Combine(newSubscriptionFolderPath, "subscriptioninformation.json");
        var subscriptionJson = JsonConvert.SerializeObject(subscriptionData, Formatting.Indented);
        File.WriteAllText(subscriptionJsonFilePath, subscriptionJson);
    }

    public void AddOrUpdateEndpointsToApis(IEnumerable<string> apis, IEnumerable<string> endpoints)
    {
        foreach (var api in apis)
        {
            var specificationFilePath = Path.Combine(ApiFolderPath, api, "specification.json");

            var apiData = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(specificationFilePath));

            if (apiData == null)
            {
                throw new Exception($"specifications.json does not exists at {Path.Combine(ApiFolderPath, api)}");
            }

            if (!apiData.ContainsKey("paths"))
            {
                apiData["paths"] = new JObject();
            }

            var paths = (JObject)apiData["paths"];

            var availableEndpoints = ListEndpoints();

            foreach (var endpoint in endpoints)
            {
                EndpointDetail endpointDetail = availableEndpoints.First(e => e.FunctionName == endpoint);
                paths[endpointDetail.Route] = CreatePathObject(endpointDetail);

                string folderName = $"{endpointDetail.Method.ToLower()}-{endpointDetail.FunctionName.ToLower()}";
                string folderPath = Path.Combine(ApiFolderPath, "operations", folderName);
                Directory.CreateDirectory(folderPath);

                string xmlFilePath = Path.Combine(folderPath, "policy.xml");

                var xmlContent = new XElement("policies",
                    new XElement("inbound",
                        new XElement("base"),
                        new XElement("set-backend-service",
                            new XAttribute("id", "apim-generated-policy"),
                            new XAttribute("backend-id", "ychapiv3")
                        )
                    ),
                    new XElement("backend",
                        new XElement("base")
                    ),
                    new XElement("outbound",
                        new XElement("base")
                    ),
                    new XElement("on-error",
                        new XElement("base")
                    )
                );

                xmlContent.Save(xmlFilePath);
            }

            File.WriteAllText(specificationFilePath, JsonConvert.SerializeObject(apiData, Formatting.Indented));
        }
    }

    public void RemoveEndpointsFromApis(IEnumerable<string> apis, IEnumerable<string> endpoints)
    {
        foreach (var api in apis)
        {
            var specificApiFolderPath = Path.Combine(ApiFolderPath, api);
            var specificationFilePath = Path.Combine(specificApiFolderPath, "specification.json");

            if (!File.Exists(specificationFilePath))
            {
                Console.WriteLine($"Api {api} does not have a specification file at it's root. Skipping.",
                    ConsoleColor.Yellow);
                continue;
            }

            var apiData =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(specificationFilePath));

            if (apiData == null || !apiData.ContainsKey("paths"))
            {
                Console.WriteLine($"Api {api} does not have a paths section of it's specification file. Skipping.",
                    ConsoleColor.Yellow);
                continue;
            }

            var paths = (JObject)apiData["paths"];

            var availableEndpoints = ListEndpoints();

            foreach (var endpoint in endpoints)
            {
                var endpointDetail = availableEndpoints.FirstOrDefault(e => e.FunctionName == endpoint);
                if (endpointDetail == null)
                {
                    Console.WriteLine($"Endpoint {endpoint} not found in function app. Skipping.", ConsoleColor.Yellow);
                    continue;
                }

                if (paths.ContainsKey(endpointDetail.Route))
                {
                    paths.Remove(endpointDetail.Route);
                }
                else
                {
                    Console.WriteLine(
                        $"Route {endpointDetail.Route} not found in API specification for {api}. Skipping.",
                        ConsoleColor.Yellow);
                }

                string folderName = $"{endpointDetail.Method.ToLower()}-{endpointDetail.FunctionName.ToLower()}";
                string folderPath = Path.Combine(specificApiFolderPath, "operations", folderName);
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                }
            }

            File.WriteAllText(specificationFilePath, JsonConvert.SerializeObject(apiData, Formatting.Indented));
        }
    }

    public void WriteApimDefinitionToJson()
    {
        var subscriptionsData = new JObject();
        var noSubscriptionData = new JObject();
        int totalSubscriptions = 0;

        foreach (var subscriptionFolder in Directory.GetDirectories(SubscriptionsFolderPath))
        {
            string subscriptionName = Path.GetFileName(subscriptionFolder);

            string subscriptionInfoPath = Path.Combine(subscriptionFolder, "subscriptioninformation.json");
            if (!File.Exists(subscriptionInfoPath)) continue;

            var subscriptionData = JObject.Parse(File.ReadAllText(subscriptionInfoPath));
            string scope = subscriptionData["properties"]?["scope"]?.ToString();
            if (string.IsNullOrEmpty(scope) || !scope.Contains("/apis/")) continue;

            // Extract the API name from the scope
            string apiNameFromScope = scope.Split("/apis/")[1];
            totalSubscriptions++;

            var subscriptionObject = new JObject();

            foreach (var apiFolder in Directory.GetDirectories(ApiFolderPath))
            {
                string apiFolderName = Path.GetFileName(apiFolder); // Get the folder name
                string apiInfoPath = Path.Combine(apiFolder, "apiInformation.json");
                if (!File.Exists(apiInfoPath)) continue;

                var apiData = JObject.Parse(File.ReadAllText(apiInfoPath));
                string displayName = apiData["properties"]?["displayName"]?.ToString();
                string apiPath = apiData["properties"]?["path"]?.ToString();

                var apiObject = new JObject
                {
                    ["endpointCount"] = 0, // Initially set endpoint count to 0
                    ["folderName"] = apiFolderName,
                    ["path"] = apiPath
                };

                var endpointsData = new JObject();
                int apiEndpointCount = 0;

                string operationsPath = Path.Combine(apiFolder, "operations");
                var operationFolders = Directory.Exists(operationsPath)
                    ? Directory.GetDirectories(operationsPath)
                    : Array.Empty<string>();

                string specificationFilePath = Path.Combine(apiFolder, "specification.json");
                var specificationData = File.Exists(specificationFilePath)
                    ? JObject.Parse(File.ReadAllText(specificationFilePath))
                    : null;

                var paths = specificationData?["paths"] as JObject;

                foreach (var operationFolder in operationFolders)
                {
                    string operationName = Path.GetFileName(operationFolder);
                    string[] parts = operationName.Split('-', 2);
                    if (parts.Length != 2) continue;

                    string method = parts[0];
                    string endpointName = parts[1];

                    string endpointPath = paths?.Properties()
                        .FirstOrDefault(p => p.Value[method]?["operationId"]?.ToString() == operationName)?.Name;

                    bool properlySetUp = !string.IsNullOrEmpty(endpointPath);

                    var endpointData = new JObject
                    {
                        ["method"] = method,
                        ["path"] = endpointPath ?? "Unknown",
                        ["isValid"] = properlySetUp
                    };

                    endpointsData[endpointName] = endpointData;
                    apiEndpointCount++;
                }

                apiObject["endpoints"] = endpointsData;
                apiObject["endpointCount"] = apiEndpointCount;

                // Check if the API folder name matches the end of the subscription's scope
                if (apiFolderName == apiNameFromScope)
                {
                    // Add API under its corresponding subscription
                    subscriptionObject[displayName ?? "Unknown API"] = apiObject;
                }
                else
                {
                    // Add API under "no subscription"
                    noSubscriptionData[displayName ?? "Unknown API"] = apiObject;
                }
            }

            // Add subscription data
            subscriptionsData[subscriptionName] = subscriptionObject;
        }

        var finalOutput = new JObject
        {
            ["totalSubscriptions"] = totalSubscriptions,
            ["subscriptions"] = subscriptionsData,
            ["no subscription"] = noSubscriptionData
        };

        // Save to a JSON file in the root directory
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "apimDefinition.json");
        File.WriteAllText(outputPath, finalOutput.ToString(Newtonsoft.Json.Formatting.Indented));

        Console.WriteLine($"APIM definition JSON written to: {outputPath}");
    }

}