using CRUDAPP.Context;
using CRUDAPP.Contracts;
using CRUDAPP.dto;
using CRUDAPP.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDAPP.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<company>> GetCompanies()
        {
            var query = "SELECT Id, Name,Address,Country FROM Companies";

            using(var connection=_context.CreateConnection())
            {
                var companies=await connection.QueryAsync<company>(query); 
                return companies.ToList();
            }
        }
        public async Task<company>GetCompany(int id)
        {
            var query = "Select * from companies where ID=@ID ";
            using (var connection = _context.CreateConnection())
            {
                var comp = await connection.QuerySingleOrDefaultAsync<company>(query,new { id });
                return comp;
            }
        }
        public async Task<company> CreateCompany(CompanyForCreationDto company)
        {
            var query = "Insert into companies (Name,Address,Country) Values(@Name,@Address,@Country)" + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdCompany = new company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country,
                };
                return createdCompany;
                
            }
        }

        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            var query = "Update companies set Name=@Name, Address=@Address,Country=@Country where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _context.CreateConnection())
            {
               await connection.ExecuteAsync(query, parameters);
             
            }
        }

        public async Task DeleteCompany(int id)
        {
            var query = "Delete from companies where Id=@Id";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });

            }
        }
        public async Task<company> GetCompanyByEmployeeId(int id) //stored procedure
        {
            var procedureName = "ShowCompanyForProvidedEmployeeId";
            var parameters = new DynamicParameters();
            parameters.Add("Id",id,DbType.Int32,ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<company>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company;
            }

        }
        public async Task<company>GetCompanyByEmployeesMultipleResults(int id)
        {
            var query = "Select * from companies where Id=@Id;"+
                        "Select * from employees where CompanyId=@Id";

            using (var connection = _context.CreateConnection())
                using (var multi=await connection.QueryMultipleAsync(query
                    , new {id}))
            {
                var company = await multi.ReadSingleOrDefaultAsync<company>() ;
                if (company != null)
                    company.Employees = (await multi.ReadAsync<Employee>()).ToList();
                return company;
            }

        }

        public async Task<List<company>>GetCompaniesEmployeeMultiMapping()
        {
            var query = "Select * from companies c Join employees e on c.Id=e.CompanyId";

            using (var connection = _context.CreateConnection())
            {
                var companyDict = new Dictionary<int, company>();

                var companies = await connection.QueryAsync<company, Employee, company>(query, (company, Employee) =>
                {
                    if (!companyDict.TryGetValue(company.Id, out var currentCompany))
                    {
                        currentCompany = company;
                        companyDict.Add(currentCompany.Id, currentCompany);
                    }
                    currentCompany.Employees.Add(Employee);
                    return currentCompany;
                });
                return companies.Distinct().ToList();
            }

        }
    }
}
