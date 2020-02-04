using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using PracticeAPI.Helper.Interfaces;
using PracticeAPI.Helper.Models;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCorsAttribute("_myAllowSpecificOrigins")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentTask _departmentTask;

        public DepartmentController(IDepartmentTask departmentTask)
        {
            _departmentTask = departmentTask;
        }
                
        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartment()
        {
            return _departmentTask.Departments.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            var department = _departmentTask.GetDepartment(id);
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
            var updatedDepartment = _departmentTask.UpdateDepartment(id, department);
            if (updatedDepartment == null)
            {
                return NotFound();
            }
            
            return Ok(updatedDepartment);
        }

        /*
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
        */

        [HttpPost]
        public IActionResult PostDepartment(Department department)
        {
            var createdDepartment = _departmentTask.CreateDepartment(department);
            
            if (createdDepartment == null)
            {
                return NoContent();
            }

            return CreatedAtAction("GetDepartment", new { id = createdDepartment.DepartmentID }, createdDepartment);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _departmentTask.DeleteDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
    }
}
