using CompanyAPISimple.Interfaces;
using CompanyAPISimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Configuration;
using System.Data;

namespace CompanyAPISimple.Helpers
{
    public class DataHandler : IDataHandler
    {
        public string CreateCompany(CompanyModel company)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {
                var output = connection.ExecuteReader("dbo.CreateCompany @Isin, @Name, @Exchange, @Ticker, @website", new { Isin = company.Isin, Name = company.name, Exchange = company.exchange, Ticker = company.ticker, website = company.website });
                return output.GetString(0);
            }
        }

        public List<CompanyModel> GetCompanies()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {
                var output = connection.Query<CompanyModel>("dbo.GetCompanies").ToList();
                return output;
            }
        }

        public CompanyModel GetCompanyById(int Id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {
                CompanyModel output = connection.QuerySingleOrDefault<CompanyModel>("dbo.GetCompanyById @id", new { id = Id });
                return output;
            }
        }

        public CompanyModel GetCompanyByISIN(string ISIN)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {                
                var output = connection.QuerySingleOrDefault<CompanyModel>("dbo.GetCompanyByISIN @isin", new { isin = ISIN });
                return output;
            }
        }

        public string UpdateCompany(CompanyModel company)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {
                var output = connection.ExecuteReader("dbo.UpdateCompany @Isin, @Name, @Exchange, @Ticker, @website", new { Isin = company.Isin, Name = company.name, Exchange = company.exchange, Ticker = company.ticker, website = company.website });
                return output.GetString(0);
            }
        }
    }
}