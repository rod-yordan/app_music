using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("force-error")]
        public IActionResult ForceError()
        {
            throw new Exception("Este es un error de prueba forzado");
        }

        [HttpGet("force-divide-zero")]
        public IActionResult ForceDivideByZero()
        {
            int zero = 0;
            int result = 100 / zero;
            return Ok(result);
        }

        [HttpGet("force-null")]
        public IActionResult ForceNullReference()
        {
            string? nullString = null;
            var length = nullString.Length;
            return Ok(length);
        }
    }
}