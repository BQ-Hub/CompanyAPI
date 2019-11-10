using CompanyAPISimple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAPISimple.Interfaces
{
    public interface IDataHandler
    {
        List<CompanyModel> GetCompanies();

        CompanyModel GetCompanyById(int Id);

        CompanyModel GetCompanyByISIN(string isin);

        string CreateCompany(CompanyModel company);

        string UpdateCompany(CompanyModel company);
    }
}
