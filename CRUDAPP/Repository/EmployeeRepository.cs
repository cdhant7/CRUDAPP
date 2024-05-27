using CRUDAPP.Context;
using CRUDAPP.Contracts;
using CRUDAPP.dto;
using CRUDAPP.Entities;
using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Connections;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CRUDAPP.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>>GetEmployee()
        {
            var query = "select *from employee";
            using (var connection = _context.CreateConnection())
            { 
                var employees=await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }
            
        }

        public Task<List<Employee>>GetEmployeebyMultimapping()
        {
            var query = " ";
        }
    }
}

