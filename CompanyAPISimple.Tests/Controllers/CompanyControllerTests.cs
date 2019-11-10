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
            IEnumerable<CompanyModel> result = controller.GetCompanies();

            // Assert
            Assert.IsNotNull(result);
            
            Assert.AreEqual(5, result.Count());
        }

        [TestMethod()]
        public void GetCompanyByIdTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            CompanyModel expectedCompany = new CompanyModel { Isin = "US0378331005", exchange = "NASDAQ", name = "Apple Inc.", ticker = "AAPL", website = "http://www.apple.com" };

            // Act
            CompanyModel actualCompany = controller.GetCompanyById(2);

            // Assert
            Assert.IsNotNull(actualCompany);
            
            Assert.AreEqual(expectedCompany, actualCompany);
        }

        [TestMethod()]
        public void GetCompanyByISINTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            CompanyModel expectedCompany = new CompanyModel { Isin = "US0378331005", exchange = "NASDAQ", name = "Apple Inc.", ticker = "AAPL", website = "http://www.apple.com" };

            // Act
            CompanyModel actualCompany = controller.GetCompanyByISIN("US0378331005");

            // Assert
            Assert.IsNotNull(actualCompany);
            
            Assert.AreEqual(expectedCompany, actualCompany);            
            
        }

        [TestMethod()]
        public void CreateCompanyTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            IEnumerable<CompanyModel> companyList = controller.GetCompanies();
            CompanyModel company = new CompanyModel { Isin = "IE1234567899", exchange = "ISEQ", name = "Brian Inc", ticker = "BAJQ", website = "https://www.startrek.com" };

            // Act
            var result = controller.CreateCompany(company);
            result.GetType();
            // Assert
            Assert.IsTrue(companyList.Contains(company));
        }

        [TestMethod()]
        public void CreateExistingCompanyTest()
        {
            // Arrange
            CompanyController controller = new CompanyController(new TestDataHandler());
            IEnumerable<CompanyModel> companyList = controller.GetCompanies();            

            // Act
            var result = controller.CreateCompany(companyList.First());

            // Assert
            //Assert.IsTrue(companyList.Contains(company));
            Assert.IsNotNull(result);

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

            CompanyModel actualCompany = controller.GetCompanyByISIN("IE1234567899");

            // Assert
            Assert.AreEqual(expectedCompany.Isin, actualCompany.Isin);
            Assert.AreNotEqual(expectedCompany, actualCompany);            
        }

        public List<CompanyModel> LoadTestData()
        {
            List<CompanyModel> companyList = new List<CompanyModel>();

            using (StreamReader r = new StreamReader("./Data/TestData.json"))
            {
                string json = r.ReadToEnd();
                companyList = JsonConvert.DeserializeObject<List<CompanyModel>>(json);
            }

            return companyList;
        }
    }
}