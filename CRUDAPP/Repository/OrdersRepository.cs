using CRUDAPP.Context;
using CRUDAPP.Contracts;
using CRUDAPP.dto;
using CRUDAPP.Entities;
using Dapper;
using Microsoft.AspNetCore.Connections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CRUDAPP.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DapperContext _context;

        public OrdersRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderByMultiMapping()
        {
            var query = @"
                SELECT o.OrderId, o.[Status], c.CustomerId, c.[Name]
                FROM Orders o
                INNER JOIN Customers c ON o.CustomerId = c.CustomerId
                WHERE o.OrderId =2";

            using (var connection = _context.CreateConnection())
            {
                var orderDict = new Dictionary<int, Order>();

                var orders = await connection.QueryAsync<Order, Customer, Order>(
                    query,
                    (order, customer) =>
                    {
                        if (!orderDict.TryGetValue(order.orderid, out var currentOrder))
                        {
                            currentOrder = order;
                            orderDict.Add(currentOrder.orderid, currentOrder);
                        }
                        currentOrder.Customer = customer;
                        return currentOrder;
                    },
                    splitOn: "CustomerId");

                return orderDict.Values.ToList();
            }
        }

        public async Task<List<Order>>GetOrderwithstores()
        {
            var query = @"  SELECT o.OrderId, o.[Status], c.CustomerId, c.[Name], s.StoreId, s.[Location]
                        FROM Orders o
                        INNER JOIN Customers c
                        ON o.CustomerId = c.CustomerId
                        INNER JOIN Stores s
                        ON o.StoreId = s.StoreId
                        WHERE o.OrderId = 2";
            
            using (var connection= _context.CreateConnection())
            {
                var orderDict= new Dictionary<int, Order>();
                var orders = await connection.QueryAsync<Order, Customer, Store, Order>(
                    query, (order, Customer, Store) =>
                    {
                        return new Order
                        {
                            orderid = order.orderid,
                            status = order.status,
                            Customer = new Customer
                            {
                                CustomerId = Customer.CustomerId,
                                Name = Customer.Name
                            },
                            Store = new Store
                            {
                                StoreId = Store.StoreId,
                                Location = Store.Location
                            }
                        };
                    },
                    splitOn: "CustomerId,StoreId");

                return orders.ToList();
            }
        }
    }
}

