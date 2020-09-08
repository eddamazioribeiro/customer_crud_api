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
    public class AddressController : ControllerBase
    {
        public AddressRepository _repo { get; }

        public AddressController(AddressRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("{customerId}")]
        public async Task<IActionResult> CreateAddress(int customerId, Address address)
        {
            try
            {
                address.CustomerId = customerId;

                _repo.Create(address);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest("Não foi possível salvar as informações");
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, Address address)
        {
            try
            {
                var addressAux = await _repo.GetAddressById(addressId);

                if (addressAux == null)
                {
                    return NotFound();
                }
                
                _repo.Update(address);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest("Não foi possível salvar as informações");
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            try
            {
                var addressAux = await _repo.GetAddressById(addressId);


                if (addressAux == null)
                {
                    return NotFound();
                }
                else
                {
                    _repo.Delete(addressAux);

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

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            try
            {
                var address = await _repo.GetAddressById(addressId);

                return Ok(address);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("list/{customerId}")]
        public async Task<IActionResult> ListAllAddresses(int? customerId)
        {
            try
            {
                var addresses = await _repo.GetAllAddresses(customerId);

                return Ok(addresses);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}