using CRUDAPP.Contracts;
using CRUDAPP.dto;
using CRUDAPP.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDAPP.Controllers
{
    [Route("api/mapping")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _OrderRepo;

        public OrdersController(IOrdersRepository OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }

        [HttpGet("multiplemapping")] // this is for executing multiple mapping
        public async Task<ActionResult<List<Order>>> GetOrderByMultiMapping()
        {
            try
            {
                var orders = await _OrderRepo.GetOrderByMultiMapping();
                if (orders == null || !orders.Any())
                    return NotFound();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("multipleorder")] // this is for executing multiple mapping with multiple objects
        public async Task<ActionResult<List<Order>>>GetOrderwithstores()
        {
            try
            {
                var orders = await _OrderRepo.GetOrderwithstores();
                if (orders == null || !orders.Any())
                    return NotFound();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("dynamicparameter")] // this is for executing multiple mapping with multiple objects
        public async Task<ActionResult<List<Order>>>GetDynamicParameter()
        {
            try
            {
                var orders = await _OrderRepo.GetDynamicParameter();
                if (orders == null || !orders.Any())
                    return NotFound();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

