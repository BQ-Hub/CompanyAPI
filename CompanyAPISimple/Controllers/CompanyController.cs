using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CompanyAPISimple.Models;
using CompanyAPISimple.Interfaces;

namespace CompanyAPISimple.Controllers
{
    
    [RoutePrefix("api/Company")]
    public class CompanyController : ApiController
    {
        private IDataHandler _dataHandler;

        public CompanyController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }
        public List<CompanyModel> GetCompanies()
        {

            return _dataHandler.GetCompanies();
        }

        [Route("{id:int}")]
        [HttpGet]
        public CompanyModel GetCompanyById(int Id)
        {           
            return _dataHandler.GetCompanyById(Id);
        }

        [Route("{isin}")]
        [HttpGet]
        public CompanyModel GetCompanyByISIN(string isin)
        {
            return _dataHandler.GetCompanyByISIN(isin);
        }

        [Route("CreateCompany")]
        [HttpPost]
        public IHttpActionResult CreateCompany(CompanyModel company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (GetCompanyByISIN(company.Isin)?.Isin == null)
            {
                _dataHandler.CreateCompany(company);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Company already exists");
            }

            return Ok();
        }

        [Route("UpdateCompany")]
        [HttpPost]
        public IHttpActionResult UpdateCompany(CompanyModel company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (GetCompanyByISIN(company.Isin)?.Isin != null)
            {
                _dataHandler.UpdateCompany(company);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "Company does not exist");
            }

            return Ok();
        }
    }
}
