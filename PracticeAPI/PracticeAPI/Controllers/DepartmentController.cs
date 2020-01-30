using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeMIS.Models;
using PracticeAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCorsAttribute("_myAllowSpecificOrigins")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeMISContext _context;

        public DepartmentController(EmployeeMISContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartment()
        {
            return _context.Department.ToListAsync().Result;
        }

        [HttpGet("{id}")]
        public ActionResult GetDepartment(int id)
        {
            /*var department = await _context.Department.FindAsync(id);*/
            var department = _context.Department.Find(id);
            var departmentT = _context.Department.Include(department => department.Employees)
                .FirstOrDefault(department => department.DepartmentID == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentID)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return NoContent();
            return Ok(department);
        }

        [HttpGet("search/{searchString}")]
        public ActionResult SearchDepartments(string searchString)
        {
            var departments = from dep in _context.Department.Include(dep => dep.Employees) select dep;

            if (!string.IsNullOrEmpty(searchString))
            {
                var department = departments.Where(department => department.DepartmentName.Contains(searchString));

                if (!department.Any())
                {
                    return NotFound();
                }

                return Ok(department.ToList());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<Department> PostDepartment(Department department)
        {
            _context.Department.Add(department);
            _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentID }, department);
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartment(int id)
        {
            var department = _context.Department.FindAsync(id).Result;
            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            _context.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentID == id);
        }
    }
}
