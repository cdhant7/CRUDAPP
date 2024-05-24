using CRUDAPP.dto;
using CRUDAPP.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDAPP.Contracts
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>>GetEmployee();
    }
}
