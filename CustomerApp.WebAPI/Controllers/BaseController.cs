using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private static readonly string Message = "Customer CRUD";

        public BaseController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Message);
        }
    }
}
