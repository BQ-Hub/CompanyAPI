using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyAPISimple.Interfaces;
using CompanyAPISimple.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CompanyAPISimple.Tests.Helpers
{
    public class TestDataHandler : IDataHandler
    {
        List<CompanyModel> companyList = new List<CompanyModel>();

        /// <summary>
        /// default constructor for the TestDataHandler
        /// </summary>
        public TestDataHandler()
        {
            using (StreamReader r = new StreamReader("./Data/TestData.json"))
            {
                string json = r.ReadToEnd();
                companyList = JsonConvert.DeserializeObject<List<CompanyModel>>(json);
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        public string CreateCompany(CompanyModel company)
        {
            string retval = "";

            if (!companyList.Exists(c => c.Isin == company.Isin))
            {
                companyList.Add(company);
            }
            else
            {
                retval = "Company does not exist";
            }            

            return retval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompanyModel> GetCompanies()
        {
            return companyList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CompanyModel GetCompanyById(int Id)
        {
            CompanyModel retCompany = new CompanyModel();

            using (StreamReader r = new StreamReader("./Data/TestData.json"))
            {
                string json = r.ReadToEnd();

                List<JObject> list = JsonConvert.DeserializeObject<List<JObject>>(json);
                
                retCompany = list.First(obj => obj["ID"].Value<int>().Equals(Id)).ToObject<CompanyModel>();
            }
            
            return retCompany;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        public CompanyModel GetCompanyByISIN(string isin)
        {
            return companyList.Find(c=>c.Isin.Equals(isin, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        public string UpdateCompany(CompanyModel company)
        {
            string retval = "";

            if(companyList.Exists(c => c.Isin == company.Isin))
            {
                companyList.Insert(companyList.FindIndex(c=>c.Isin.Equals(company.Isin, StringComparison.InvariantCultureIgnoreCase)), company);
            }
            else
            {
                retval = "Company does not exist";
            }

            return retval;
        }
    }
}
