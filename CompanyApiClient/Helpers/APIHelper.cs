using CompanyAPISimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace CompanyApiClient.Helpers
{
    public static class APIHelper
    {
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["CompanyAPIURL"];
        public static async Task<IEnumerable<CompanyModel>> GetAllCompaniesAsync()
        {
            
            List<CompanyModel> companies = new List<CompanyModel>();
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await GetApiRequestAsync($"{BaseUrl}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                companies = await response.Content.ReadAsAsync<List<CompanyModel>>();
            }

            return companies;
        }

        public static async Task<CompanyModel> GetCompanyByIdAsync(int Id)
        {
            HttpClient client = new HttpClient();
            CompanyModel company = new CompanyModel();

            HttpResponseMessage response = await client.GetAsync($"{BaseUrl}{Id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                company = await response.Content.ReadAsAsync<CompanyModel>();
            }

            return company;
        }

        public static async Task<CompanyModel> GetCompanyByISINAsync(string isin)
        {
            HttpClient client = new HttpClient();
            CompanyModel company = new CompanyModel();

            HttpResponseMessage response = await client.GetAsync($"{BaseUrl}{isin}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                company = await response.Content.ReadAsAsync<CompanyModel>();
            }

            return company;
        }

        public static async Task<string> CreateCompany(CompanyModel company)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = new StringContent(JsonConvert.SerializeObject(company), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/CreateCompany", json).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    company = await response.Content.ReadAsAsync<CompanyModel>();
                }

            }

            return "";
        }

        public static async Task<string> UpdateCompany(CompanyModel company)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent json = new StringContent(JsonConvert.SerializeObject(company), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/UpdateCompany", json).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    company = await response.Content.ReadAsAsync<CompanyModel>();
                }

            }

            return "";
        }

        private static async Task<HttpResponseMessage> GetApiRequestAsync(string Url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync($"{Url}").ConfigureAwait(false);
            }
        }

        private static async Task<HttpResponseMessage> PostApiRequestAsync(string Url, StringContent json)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.PostAsync($"{BaseUrl}", json).ConfigureAwait(false);
            }
        }
    }
}