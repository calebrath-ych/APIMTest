using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Ych.Api;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddMvc().AddNewtonsoftJson();

        ServiceManager.Initialize();
        ServiceManager.RegisterServices(services);

        services.Configure<WorkerOptions>(workerOptions =>
        {
            workerOptions.Serializer = new NewtonsoftJsonObjectSerializer(JsonConvert.DefaultSettings());
        });
    })
    .Build();

await host.RunAsync();
