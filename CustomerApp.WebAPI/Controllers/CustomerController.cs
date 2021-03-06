using System;
using System.Threading.Tasks;
using CustomerApp.Domain.Model;
using CustomerApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        public CustomerRepository _repo { get; }

        public CustomerController(CustomerRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            try
            {
                customer.CreatedAt = DateTime.Now;

                _repo.Create(customer);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok(customer);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest("Não foi possível salvar as informações");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            try
            {
                var customerAux = await _repo.GetCustomerByIdAsync(customer.Id, false);

                if (customerAux == null)
                {
                    return NotFound();
                }
                else
                {
                    customerAux.Name = customer.Name;
                    customerAux.EMail = customer.EMail;
                    customerAux.BirthDate = customer.BirthDate;
                    customerAux.UpdatedAt = DateTime.Now;
                    
                    _repo.Update(customerAux);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok(customerAux);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest("Não foi possível salvar as informações");
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            try
            {
                var customerAux = await _repo.GetCustomerByIdAsync(customerId, false);

                if (customerAux == null)
                {
                    return NotFound();
                }
                else
                {
                    _repo.Delete(customerAux);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok();
                    }
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest("Não foi possível excluir as informações");
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                var customer = await _repo.GetCustomerByIdAsync(customerId, true);

                return Ok(customer);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetCustomersByNameAsync(string name)
        {
            try
            {
                var customers = await _repo.GetAllCustomersByNameAsync(name);

                return Ok(customers);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListAllCustomersAsync()
        {
            try
            {
                var customers = await _repo.GetAllCustomersAsync(false);

                return Ok(customers);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}