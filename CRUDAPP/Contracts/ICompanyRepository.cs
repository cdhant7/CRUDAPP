using CRUDAPP.dto;
using CRUDAPP.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDAPP.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<company>> GetCompanies();
        public Task<company>GetCompany(int id);
        public Task <company>CreateCompany(CompanyForCreationDto company);
        public Task UpdateCompany(int id ,CompanyForUpdateDto company);
        public Task DeleteCompany(int id);
        public Task<company>GetCompanyByEmployeeId(int id);
        public Task<company>GetCompanyByEmployeesMultipleResults(int id);
        public Task<List<company>>GetCompaniesEmployeeMultiMapping();
    }
}
