using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyAPISimple.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyAPISimple.Tests.Helpers;
using CompanyAPISimple.Models;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Net;
using System.Web.Http.Results;
using System.Web.Http;

namespace CompanyAPISimple.Controllers.Tests
{
    [TestClass()]
    public class CompanyControllerTests
    {        
        [TestMethod()]
        public void GetCompanysTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            // Act
            IHttpActionResult actionResult = controller.GetCompanies();
            
            OkNegotiatedContentResult<List<CompanyModel>> contentResult = actionResult as OkNegotiatedContentResult<List<CompanyModel>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);           
            Assert.AreEqual(5, contentResult.Content.Count());
        }

        [TestMethod()]
        public void GetCompanyByIdTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            CompanyModel expectedCompany = new CompanyModel { Isin = "US0378331005", exchange = "NASDAQ", name = "Apple Inc.", ticker = "AAPL", website = "http://www.apple.com" };

            // Act
            IHttpActionResult actionResult = controller.GetCompanyById(2);
            OkNegotiatedContentResult<CompanyModel> actualCompany = actionResult as OkNegotiatedContentResult<CompanyModel>;

            // Assert
            Assert.IsNotNull(actualCompany);
            
            Assert.AreEqual(expectedCompany, actualCompany.Content);
        }
        
        [TestMethod()]
        public void GetCompanyByISINTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            CompanyModel expectedCompany = new CompanyModel { Isin = "US0378331005", exchange = "NASDAQ", name = "Apple Inc.", ticker = "AAPL", website = "http://www.apple.com" };

            // Act
            IHttpActionResult actionResult = controller.GetCompanyByISIN("US0378331005");
            OkNegotiatedContentResult<CompanyModel> actualCompany = actionResult as OkNegotiatedContentResult<CompanyModel>;

            // Assert
            Assert.IsNotNull(actualCompany);
            Assert.AreEqual("US0378331005", actualCompany.Content.Isin);
            Assert.AreEqual(expectedCompany, actualCompany.Content);            
            
        }

        [TestMethod()]
        public void CreateCompanyTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());            
            CompanyModel company = new CompanyModel { Isin = "IE1234567899", exchange = "ISEQ", name = "Brian Inc", ticker = "BAJQ", website = "https://www.startrek.com" };

            // Act
            var result = controller.CreateCompany(company);
            IHttpActionResult actionResult = controller.GetCompanies();

            OkNegotiatedContentResult<List<CompanyModel>> companyList = actionResult as OkNegotiatedContentResult<List<CompanyModel>>;

            // Assert
            Assert.IsTrue(companyList.Content.Contains(company));
        }

        [TestMethod()]
        public void CreateExistingCompanyTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            IHttpActionResult expectedActionResult = controller.GetCompanies();

            OkNegotiatedContentResult<List<CompanyModel>> expectedCompanyList = expectedActionResult as OkNegotiatedContentResult<List<CompanyModel>>;

            int expectedCount = expectedCompanyList.Content.Count;

            // Act
            // Try to add an existing company to the list
            IHttpActionResult result = controller.CreateCompany(expectedCompanyList.Content.First());
            NegotiatedContentResult<string> response = result as NegotiatedContentResult<string>;

            IHttpActionResult actualActionResult = controller.GetCompanies();
            OkNegotiatedContentResult<List<CompanyModel>> actualCompanyList = actualActionResult as OkNegotiatedContentResult<List<CompanyModel>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
            Assert.IsTrue(actualCompanyList.Content.Count == expectedCount);

        }

        [TestMethod()]
        public void UpdateCompanyTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            CompanyModel expectedCompany = new CompanyModel { Isin = "IE1234567899", exchange = "ISEQ", name = "Brian Inc", ticker = "BAJQ", website = "https://www.startrek.com" };
            controller.CreateCompany(expectedCompany);

            // Act            
            var result = controller.UpdateCompany(new CompanyModel { Isin = "IE1234567899", exchange = "NASDAQ", name = "Brian Q Inc", ticker = "BAJQ", website = "https://www.starwars.com" });
            IHttpActionResult actionResult = controller.GetCompanyByISIN("IE1234567899");
            OkNegotiatedContentResult<CompanyModel> actualCompany = actionResult as OkNegotiatedContentResult<CompanyModel>;

            // Assert
            Assert.AreEqual(expectedCompany.Isin, actualCompany.Content.Isin);
            Assert.AreNotEqual(expectedCompany, actualCompany.Content);
        }

        [TestMethod()]
        public void UpdateNonExistentCompanyTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());

            // Act            
            IHttpActionResult result = controller.UpdateCompany(new CompanyModel { Isin = "IE1234567899", exchange = "NASDAQ", name = "Brian Q Inc", ticker = "BAJQ", website = "https://www.starwars.com" });
            NegotiatedContentResult<string> response = result as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);

        }

    }
}