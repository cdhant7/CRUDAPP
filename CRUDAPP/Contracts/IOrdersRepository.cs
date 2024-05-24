using CRUDAPP.dto;
using CRUDAPP.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDAPP.Contracts
{
    public interface IOrdersRepository
    {
        public Task<List<Order>>GetOrderByMultiMapping();
        public Task<List<Order>> GetOrderwithstores();
    }
}
