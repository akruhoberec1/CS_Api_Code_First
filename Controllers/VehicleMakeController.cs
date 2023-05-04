using API_CSharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<VehicleMake>>> Get()
        {
            var makes = new List<VehicleMake>
            {
                new VehicleMake { Id = 1, Name = "Volkswagen", Abrv = "VW"}
            };

            return Ok(makes);
        }
    }
}
