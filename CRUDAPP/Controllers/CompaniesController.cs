using CRUDAPP.Contracts;
using CRUDAPP.dto;
using CRUDAPP.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CRUDAPP.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public async Task<ActionResult>GetCompanies()
        {
            try
            {
                var company = await _companyRepo.GetCompanies();
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult>GetCompany(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompany( id);
                if (company == null)
                {
                    return NotFound();
                }

                return Ok (company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult>CreateCompany(CompanyForCreationDto company)
        {
            try
            {
                var createdcompany = await _companyRepo.CreateCompany(company);
                return CreatedAtRoute("CompanyById", new {id=createdcompany.Id},createdcompany);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateCompany(int id,CompanyForUpdateDto company)
        {
            try
            {
                var dbcompany = await _companyRepo.GetCompany(id);
                if (dbcompany == null)
                
                    return NotFound();

                await _companyRepo.UpdateCompany(id, company);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult>DeleteCompany(int id)
        {
            try
            {
                var dbcompany = await _companyRepo.GetCompany(id);
                if (dbcompany == null)

                    return NotFound();

                await _companyRepo.DeleteCompany(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("ByEmployeeId/{id}")] //through stored procedure
        public async Task<ActionResult>GetCompanyForEmployee(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompanyByEmployeeId(id);
                if (company == null)

                    return NotFound();

                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/MultipleResult")] //this is for executing multing sqlstatement in one query
        public async Task<ActionResult>GetCompanyByEmployeesMultipleResults(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompanyByEmployeesMultipleResults(id);
                if (company == null)

                    return NotFound();

                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("MultipleMapping")] //this is for executing multiplemapping
        public async Task<ActionResult> GetCompaniesEmployeeMultiMapping()
        {
            try
            {
                var company = await _companyRepo.GetCompaniesEmployeeMultiMapping();
                if (company == null)

                    return NotFound();

                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
