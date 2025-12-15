using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeAddressesController(TypeAddressService typeAddressService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<Response<List<TypeAddressDetails>>>> GetAllTypeAddresses()
        {
            var response = await typeAddressService.GetAllAddresseTypesAsync();
            if (response.Status == 200)
            {
                return Ok(response);
            }
            return StatusCode(response.Status, response);
        }
    }
}
