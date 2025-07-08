using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.APIs.DTO;
using Web.APIs.Models;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Count")]
        public ActionResult<List<DepartmentWithEmployeeCountDTO>> GetCount()
        {
            IEnumerable<Department> departments = _context.Departments.Include(d => d.Employees).ToList();
            List<DepartmentWithEmployeeCountDTO> DTOs = new List<DepartmentWithEmployeeCountDTO>();
            foreach(Department department in departments)
            {
                DepartmentWithEmployeeCountDTO dto = new DepartmentWithEmployeeCountDTO();
                dto.NumberOfEmployees = department.Employees.Count();
                dto.Name = department.Name;
                dto.Id = department.Id;
                DTOs.Add(dto);
            }
            return DTOs;

        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult DisplayAll()
        {
            IEnumerable<Department> departments = _context.Departments.ToList();
            return Ok(departments);

        }

        [HttpGet]
        [Route("{id:int}")] // api/department/id
        public IActionResult GetById(int id)
        {
            Department? department =
                _context.Departments.FirstOrDefault(d => d.Id == id);
            return Ok(department);

        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Department? department =
                _context.Departments.FirstOrDefault(d => d.Name.ToLower() == name.ToLower());
            return Ok(department);

        }

        [HttpPost]
        public IActionResult Add(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return CreatedAtAction("GetById" , new { id = department.Id }, department);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Department department)
        {
            Department? departmentFromDb =
                _context.Departments.FirstOrDefault(d => d.Id == id);
            if (departmentFromDb == null)
                return NotFound("Department Not Found");

            departmentFromDb.Name = department.Name;
            departmentFromDb.ManagerName = department.ManagerName;
            _context.SaveChanges();

            return NoContent();

        }

        
    }
}
