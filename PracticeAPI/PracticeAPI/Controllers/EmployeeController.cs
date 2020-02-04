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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeTask _employeeTask;

        public EmployeeController(IEmployeeTask employeeTask)
        {
            _employeeTask = employeeTask;
        }

        [EnableCorsAttribute("_myAllowSpecificOrigins")]
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployee([FromQuery]Pagination pagination) => Ok(_employeeTask.GetEmployees(pagination));
        

        [HttpGet("{id}")]
        public IActionResult GetEmployee(long id)
        {
            /*var employee = await _context.Employee.FindAsync(id);*/
            var employee = _employeeTask.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("search/{searchString}")]
        public ActionResult GetAnEmployee(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var employees = _employeeTask.FindEmployees(searchString);

                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees.ToList());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("count")]
        public int GetEmployeesPerPageCount()
        {
            return _employeeTask.GetEmployeeCount();
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(long id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            var updatedEmployee = _employeeTask.UpdateEmployee(id, employee);

            return Ok(updatedEmployee);
        }

        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            var newEmployee = _employeeTask.CreateEmployee(employee);
            if(newEmployee == null)
            {
                return NoContent();
            }
            return CreatedAtAction("GetEmployee", new { id = newEmployee.EmployeeID }, newEmployee);
        }

        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(long id)
        {
            var employee = _employeeTask.DeleteEmployee(id);
            return Ok(employee);
        }
    }
}
