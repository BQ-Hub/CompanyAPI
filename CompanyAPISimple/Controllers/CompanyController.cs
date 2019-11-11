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
        public IHttpActionResult GetCompanies()
        {
            List<CompanyModel> companies = _dataHandler.GetCompanies();
            if (companies == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(companies);
            }
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetCompanyById(int Id)
        {           
            CompanyModel company = _dataHandler.GetCompanyById(Id);
            if(company == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(company);
            }           
        }

        [Route("{isin}")]
        [HttpGet]
        public IHttpActionResult GetCompanyByISIN(string isin)
        {
            CompanyModel company = _dataHandler.GetCompanyByISIN(isin);
            if (company == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(company);
            }
        }

        [Route("CreateCompany")]
        [HttpPost]
        public IHttpActionResult CreateCompany(CompanyModel company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dataHandler.GetCompanyByISIN(company.Isin) == null)
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
            
            if (_dataHandler.GetCompanyByISIN(company.Isin) != null)
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
