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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _OrderRepo;

        public EmployeeController(IEmployeeRepository OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }


        [HttpGet("employee")] 
        public async Task<ActionResult<List<Employee>>>GetEmployee()
        {
            try
            {
                var orders = await _OrderRepo.GetEmployee();
                if (orders == null || !orders.Any())
                    return NotFound();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("multimap")]
        public async Task<ActionResult<List<Employee>>>GetEmployeeByMultimapping()
        {
            try
            {
                var orders = await _OrderRepo.GetEmployee();
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

