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
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    companies = await response.Content.ReadAsAsync<List<CompanyModel>>();
                }

            }

            return companies;
        }

        public static async Task<CompanyModel> GetCompanyByIdAsync(int Id)
        {
            CompanyModel company = new CompanyModel();
            using (HttpClient client = new HttpClient())
            {                
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}{Id}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    company = await response.Content.ReadAsAsync<CompanyModel>();
                }
            }            

            return company;
        }

        public static async Task<CompanyModel> GetCompanyByISINAsync(string isin)
        {
            CompanyModel company = new CompanyModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseUrl}{isin}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    company = await response.Content.ReadAsAsync<CompanyModel>();
                }
            }
            

            return company;
        }

        public static async Task<string> CreateCompany(CompanyModel company)
        {
            string result = "";

            using (HttpClient client = new HttpClient())
            {
                var json = new StringContent(JsonConvert.SerializeObject(company), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/CreateCompany", json).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<string>();
                }

            }

            return result;
        }

        public static async Task<string> UpdateCompany(CompanyModel company)
        {
            string result = "";

            using (HttpClient client = new HttpClient())
            {
                StringContent json = new StringContent(JsonConvert.SerializeObject(company), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{BaseUrl}/UpdateCompany", json).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<string>();
                }

            }

            return result;
        }
    }
}