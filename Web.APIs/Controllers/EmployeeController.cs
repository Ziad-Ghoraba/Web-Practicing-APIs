using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.APIs.DTO;
using Web.APIs.Models;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext  _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Employee e = _context.Employees.FirstOrDefault(e => e.Id == id);
            GeneralResponse generalResponse = new GeneralResponse();
            if(e != null)
            {
                generalResponse.IsSuccess = true;
                generalResponse.Data = e;
            }
            else
            {
                generalResponse.IsSuccess = false;
                generalResponse.Data = "Id Invalid";
            }
            return Ok(generalResponse);

        }
    }
}
