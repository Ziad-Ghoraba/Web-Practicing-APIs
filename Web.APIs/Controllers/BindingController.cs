using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.APIs.Models;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpGet("{name:alpha}/{age:int}")]
        public IActionResult TestPrimitive(int age, string? name)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult TestObject(Department department, string name)
        {
            return Ok();
        }

        [HttpGet("{Id}/{Name}/{ManagerName}")]
        public IActionResult TestCustomObject([FromRoute] Department department)
        {
            return Ok();
        }
    }
}
