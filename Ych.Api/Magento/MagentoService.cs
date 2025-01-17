using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api.Data;
using Ych.Api.Data.Magento;
using Ych.Api.Data.Pim;
using Ych.Configuration;
using Ych.Logging;
using Ych.Magento;
using Ych.Magento.Models;

namespace Ych.Api.Magento
{
    /// <summary>
    /// Contract for a DI compatible component/service. DI requires an interface and one or more implementations.
    /// For single implementation contracts, the interface and class can remain in the same file for simplicity.
    /// For interfaces with multiple implementations, the interface and all implementations should exist in separate files.
    /// </summary>
    public interface IMagentoService : IApiSystemService, ICustomerContactsService, ICustomerAddressesService, IHealthyService
    {
        Task<MagentoCompanyData[]> GetCompany(string companyId);
        Task<IEnumerable> GetMagentoProductImagesByVariety(string varietyCode);
    }

    /// <summary>
    /// Implementation of MagentoService contract. Business logic should exist in services and components like these
    /// rather than directly in the Azure functions. These services can then be injected into other services so long as they don't
    /// create circular dependencies.
    /// </summary>
    public class MagentoService : ApiDataService, IMagentoService
    {
        public const string MagentoSystemName = "Magento";
        public override string SystemName => MagentoSystemName;

        protected override ApiDataSource Db => db;

        private MagentoDataSource db;
        private IMagentoApiClient magentoApi;
        private ISettingsProvider settings;
        private PimDataSource pimDb;
        private ILogWriter log;

        public MagentoService(MagentoDataSource db, IMagentoApiClient magentoApi, ISettingsProvider settings, PimDataSource pimDb, ILogWriter log)
        {
            this.db = db;
            this.magentoApi = magentoApi;
            this.settings = settings;
            this.pimDb = pimDb;
            this.log = log;
        }

        public async Task<MagentoCompanyData[]> GetCompany(string companyId)
        {
            var result = await magentoApi.GetCompany(companyId).ConfigureAwait(false);

            return result?.Data?.items?.ToArray();
        }

        public async Task<IEnumerable> GetCustomerContacts(string customerCode)
        {
            try
            {
                var result = await db.SqlQueryToList(@"
                SELECT DISTINCT
                    company_advanced_customer_entity.job_title,
                    company_advanced_customer_entity.telephone as phone,
                    customer_entity.prefix,
                    customer_entity.firstname as first_name,
                    customer_entity.middlename as middle_name,
                    customer_entity.lastname as last_name,
                    customer_entity.suffix,
                    customer_entity.email,
                    customer_address_entity.street,
                    customer_address_entity.city,
                    customer_address_entity.region,
                    customer_address_entity.postcode as postal_code,
                    customer_address_entity.country_id as country,
                    customer_address_entity.telephone as address_phone
                FROM
                    `customer_entity`
                        LEFT JOIN
                    company_advanced_customer_entity ON company_advanced_customer_entity.customer_id = customer_entity.entity_id
                        LEFT JOIN
                    company ON company.entity_id = company_advanced_customer_entity.company_id
                        LEFT JOIN
                    customer_address_entity ON customer_entity.entity_id = customer_address_entity.parent_id
                WHERE
                    ych_company_id = ?", customerCode).ConfigureAwait(false);

                foreach (var item in result)
                {
                    item.Add("name", string.Join(' ', new object[]
                    {
                    item["prefix"],
                    item["first_name"],
                    item["middle_name"],
                    item["last_name"]
                    }.Where(s => !string.IsNullOrEmpty(s.ToString())).Select(s => s.ToString().Trim())));
                }

                return SetSourceSystem(result);
            }
            catch (Exception ex)
            {
                log.Error(GetType().Name, ex, additionalProps: new (string, object)[]
                {
                    (nameof(customerCode), customerCode)
                });

                return Array.Empty<object>();
            }
        }

        public async Task<IEnumerable> GetCustomerAddresses(string customerCode)
        {
            try
            {
                var result = await db.SqlQueryToList(@"
                SELECT DISTINCT
                    customer_address_entity.street,
                    customer_address_entity.city,
                    customer_address_entity.region,
                    customer_address_entity.postcode as postal_code,
                    customer_address_entity.country_id as country,
                    customer_address_entity.telephone as address_phone,
                    customer_address_entity.fax,
                    customer_address_entity.is_active,
                    customer_address_entity.created_at,
                    customer_address_entity.updated_at
                FROM
                    `customer_entity`
                        LEFT JOIN
                    company_advanced_customer_entity ON company_advanced_customer_entity.customer_id = customer_entity.entity_id
                        LEFT JOIN
                    company ON company.entity_id = company_advanced_customer_entity.company_id
                        JOIN
                    customer_address_entity ON customer_entity.entity_id = customer_address_entity.parent_id
                WHERE
                    ych_company_id = ?", customerCode).ConfigureAwait(false);

                return SetSourceSystem(result);
            }
            catch (Exception ex)
            {
                log.Error(GetType().Name, ex, additionalProps: new (string, object)[]
                {
                    (nameof(customerCode), customerCode)
                });

                return Array.Empty<object>();
            }
        }

        public async Task<IEnumerable> GetMagentoProductImagesByVariety(string varietyCode)
        {
            // region move to PIM service
            List<Dictionary<string, Object>> results = await pimDb
                .SqlQueryToList(
                    $@"SELECT variety_code FROM varieties WHERE variety_code LIKE '{varietyCode}%' AND allow_magento_sync = 1 LIMIT 1")
                .ConfigureAwait(false);


            if (!results.Any())
            {
                throw new ApiResourceNotFoundException($"No e-commerce varieties found in PIM with code"
                                                       + " identical to or containing {varietyCode}.");
            }
            //endregion  move to pim service

            string sku = "series-" + results.First()["variety_code"].ToString();
            string targetUri = "products/" + sku;
            string bearerToken = settings[Config.Settings.Api().Magento().AuthCredentials()];

            // TODO: Move this into MagentoApiClient
            RestClient restClient = new RestClient(settings[Config.Settings.Api().Magento().BaseUrl()]);
            restClient.AddDefaultHeader("Authorization", $"Bearer {bearerToken}");
            restClient.AddDefaultHeader("Content-Type", "application/json");

            RestRequest request = new RestRequest(targetUri, Method.Get);
            RestResponse response = await restClient.ExecuteAsync(request).ConfigureAwait(false);

            CheckMagentoResponseForFailure(response);

            JObject jsonData = JObject.Parse(response.Content);
            
            JArray customAttributes = (JArray)jsonData["custom_attributes"];

            if (customAttributes != null)
            {
                jsonData["cone_image"] = settings[Config.Settings.Api().Magento().AssetBaseUrl()] +
                                  GetCustomAttributeValue(customAttributes, "thumbnail");
                jsonData["ecommerce_link"] = settings[Config.Settings.Api().Magento().ECommerceBaseUrl()] +
                                  GetCustomAttributeValue(customAttributes, "url_key")+ ".html";
                jsonData["description"] = 
                                  GetCustomAttributeValue(customAttributes, "description");
            }

            return jsonData;
        }

        private string GetCustomAttributeValue(JArray customAttributes, string attributeCode)
        {
            JToken customAttribute =
                customAttributes.FirstOrDefault(s => s["attribute_code"].ToString() == attributeCode);

            return customAttribute != null ? (string)customAttribute["value"] : null;
        }

        private void CheckMagentoResponseForFailure(RestResponse response)
        {
            if ((int)response.StatusCode != 200)
            {
                throw new ApiException(response.ErrorMessage);
            }
        }
    }
}