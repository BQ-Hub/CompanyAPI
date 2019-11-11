using CompanyApiClient.Helpers;
using CompanyAPISimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CompanyApiClient.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            IEnumerable<CompanyModel> Companies = APIHelper.GetAllCompaniesAsync().Result;
            return View("Company", Companies);
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            CompanyModel Company = APIHelper.GetCompanyByIdAsync(id).Result;
            return View("Details", Company);
        }

        public async Task<ActionResult> Company()
        {
            HttpClient client = new HttpClient();
            CompanyModel company = new CompanyModel();

            //static async Task<Product> GetProductAsync(string path)
            //{
            //Product product = null;
            HttpResponseMessage response = await client.GetAsync("http://localhost:50760/api/company/2");
            if (response.IsSuccessStatusCode)
            {
                company = await response.Content.ReadAsAsync<CompanyModel>();
            }
            //return product;
            //}

            //CompanyModel company = 
            List<CompanyModel> companies = new List<CompanyModel>() { company };
            return View(companies);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CompanyModel company = new CompanyModel() { Isin = collection["Isin"], exchange = collection["exchange"], name = collection["name"], ticker = collection["ticker"], website = collection["website"] };
                string temp = APIHelper.CreateCompany(company).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Update(int id)
        {
            CompanyModel Company = APIHelper.GetCompanyByIdAsync(id).Result;
            return View("Update", Company);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            try
            {
                CompanyModel company = new CompanyModel() { Isin = collection["Isin"], exchange = collection["exchange"], name = collection["name"], ticker = collection["ticker"], website = collection["website"] };
                string temp = APIHelper.UpdateCompany(company).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }        
    }
}
