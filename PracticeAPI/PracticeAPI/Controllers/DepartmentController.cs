using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using PracticeAPI.DLL.Classes;
using PracticeAPI.DLL.Interfaces;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCorsAttribute("_myAllowSpecificOrigins")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
                
        [HttpGet]
        public ActionResult<IEnumerable<DepartmentView>> GetDepartment()
        {
            return _departmentRepository.GetDepartments().ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            /*var department = await _context.Department.FindAsync(id);*/
            // var department = _context.Department.Find(id);
            var department = _departmentRepository.GetDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, DepartmentView department)
        {
            if (id != department.DepartmentID)
            {
                return BadRequest();
            }
            var updatedDepartment = _departmentRepository.UpdateDepartment(id, department);
            if (updatedDepartment == null)
            {
                return NotFound();
            }
            
            // return NoContent();
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
        public IActionResult PostDepartment(DepartmentView department)
        {
            var createdDepartment = _departmentRepository.CreateDepartment(department);
            
            if (createdDepartment == null)
            {
                return NoContent();
            }

            return CreatedAtAction("GetDepartment", new { id = createdDepartment.DepartmentID }, createdDepartment);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _departmentRepository.DeleteDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
    }
}
