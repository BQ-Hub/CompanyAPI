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
    /// <summary>
    /// Database access implementation using Dapper
    /// </summary>
    public class DataHandler : IDataHandler
    {
        /// <summary>
        /// Calls dbo.CreateCompany
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public string CreateCompany(CompanyModel company)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {
                DynamicParameters queryParams = new DynamicParameters();

                queryParams.Add("@Isin", dbType: DbType.String, direction: ParameterDirection.Input, value: company.Isin);
                queryParams.Add("@Name", dbType: DbType.String, direction: ParameterDirection.Input, value: company.name);
                queryParams.Add("@Exchange", dbType: DbType.String, direction: ParameterDirection.Input, value: company.exchange);
                queryParams.Add("@Ticker", dbType: DbType.String, direction: ParameterDirection.Input, value: company.ticker);
                queryParams.Add("@website", dbType: DbType.String, direction: ParameterDirection.Input, value: company.website);
                queryParams.Add("@return", dbType: DbType.String, direction: ParameterDirection.Input);

                connection.Execute("dbo.CreateCompany @Isin, @Name, @Exchange, @Ticker, @website", param: queryParams);
                return queryParams.Get<string>("@return"); ;
            }
        }

        /// <summary>
        /// Get a list of all companies
        /// </summary>
        /// <returns></returns>
        public List<CompanyModel> GetCompanies()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {
                var output = connection.Query<CompanyModel>("dbo.GetCompanies").ToList();
                return output;
            }
        }

        /// <summary>
        /// Get Companies by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CompanyModel GetCompanyById(int Id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {
                CompanyModel output = connection.QuerySingleOrDefault<CompanyModel>("dbo.GetCompanyById @id", new { id = Id });
                return output;
            }
        }

        /// <summary>
        /// Get Companies by ISIN
        /// </summary>
        /// <param name="ISIN"></param>
        /// <returns></returns>
        public CompanyModel GetCompanyByISIN(string ISIN)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {                
                var output = connection.QuerySingleOrDefault<CompanyModel>("dbo.GetCompanyByISIN @isin", new { isin = ISIN });
                return output;
            }
        }

        /// <summary>
        /// Update Company. Keyed on ISIN
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public string UpdateCompany(CompanyModel company)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["GlassLewis"].ConnectionString))
            {
                DynamicParameters queryParams = new DynamicParameters();

                queryParams.Add("@Isin", dbType: DbType.String, direction: ParameterDirection.Input, value: company.Isin);
                queryParams.Add("@Name", dbType: DbType.String, direction: ParameterDirection.Input, value: company.name);
                queryParams.Add("@Exchange", dbType: DbType.String, direction: ParameterDirection.Input, value: company.exchange);
                queryParams.Add("@Ticker", dbType: DbType.String, direction: ParameterDirection.Input, value: company.ticker);
                queryParams.Add("@website", dbType: DbType.String, direction: ParameterDirection.Input, value: company.website);
                queryParams.Add("@return", dbType: DbType.String, direction: ParameterDirection.Input);

                connection.Execute("dbo.UpdateCompany @Isin, @Name, @Exchange, @Ticker, @website", param: queryParams);
                
                return queryParams.Get<string>("@return");
            }
        }
    }
}