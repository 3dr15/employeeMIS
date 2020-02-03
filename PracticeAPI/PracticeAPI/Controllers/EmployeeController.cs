using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.DLL.Classes;
using PracticeAPI.DLL.Data;
using Microsoft.AspNetCore.Cors;
using PracticeAPI.DLL.Interfaces;

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCorsAttribute("_myAllowSpecificOrigins")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [EnableCorsAttribute("_myAllowSpecificOrigins")]
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeView>> GetEmployee([FromQuery]Pagination pagination)
        {
            //return await _context.Employee.ToListAsync();
            //return await _context.Employee.Include(employee => employee.Department).ToListAsync().ConfigureAwait(true);

            // Working 
            // return _context.Employee.Include(employee => employee.Department).ToListAsync().Result;
            
            // var employees = from emp in _context.Employee select emp;

            var employees = _employeeRepository.GetEmployees(pagination);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(long id)
        {
            /*var employee = await _context.Employee.FindAsync(id);*/
            var employee = _employeeRepository.GetEmployee(id);

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
                var employees = _employeeRepository.FindEmployees(searchString);

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
            return _employeeRepository.GetEmployeeCount();
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(long id, EmployeeView employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            var updatedEmployee = _employeeRepository.UpdateEmployee(id, employee);

            return Ok(updatedEmployee);
        }

        [HttpPost]
        public ActionResult<EmployeeView> PostEmployee(EmployeeView employee)
        {
            var newEmployee = _employeeRepository.CreateEmployee(employee);
            if(newEmployee == null)
            {
                return NoContent();
            }
            return CreatedAtAction("GetEmployee", new { id = newEmployee.EmployeeID }, newEmployee);
        }

        [HttpDelete("{id}")]
        public ActionResult<EmployeeView> DeleteEmployee(long id)
        {
            var employee = _employeeRepository.DeleteEmployee(id);

            return Ok(employee);
        }
    }
}
