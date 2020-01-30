using System;
using System.IO;
using System.Text;
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
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeMISContext _context;

        public EmployeeController(EmployeeMISContext context)
        {
            _context = context;
        }

        [EnableCorsAttribute("_myAllowSpecificOrigins")]
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployee([FromQuery]Pagination pagination)
        {
            //return await _context.Employee.ToListAsync();
            //return await _context.Employee.Include(employee => employee.Department).ToListAsync().ConfigureAwait(true);

            // Working 
            // return _context.Employee.Include(employee => employee.Department).ToListAsync().Result;
            
            // var employees = from emp in _context.Employee select emp;

            var employees = _context.Employee.Include(employee => employee.Department)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync().Result;

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(long id)
        {
            /*var employee = await _context.Employee.FindAsync(id);*/
            var employee = _context.Employee.Include(employee => employee.Department)
                .FirstOrDefaultAsync(employee => employee.EmployeeID == id).Result;

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("search/{searchString}")]
        public ActionResult GetAnEmployee(string searchString)
        {
            var emp = from employees in _context.Employee.Include(employee => employee.Department) select employees;

            if (!string.IsNullOrEmpty(searchString))
            {
                var employees = emp.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));

                if (!employees.Any())
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
            int count = _context.Employee.ToList().Count();
            double numberOfPages = count / 10.0;

            return (int)Math.Round(numberOfPages, 0, MidpointRounding.AwayFromZero);

            // return ((int)Math.Ceiling(numberOfPages));
        }

        [HttpPut("{id}")]
        public IActionResult PutEmployee(long id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            employee.DocProofLink = this.FileUpload(employee.DocProofLink);
            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return NoContent();
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            employee.DocProofLink = this.FileUpload(employee.DocProofLink);
            _context.Employee.Add(employee);
            _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeID }, employee);
        }

        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(long id)
        {
            var employee = _context.Employee.FindAsync(id).Result;
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }

        private string FileUpload(string base64_file_string)
        {
            // string storage = Environment.CurrentDirectory;
            string storage = AppDomain.CurrentDomain.BaseDirectory + "\\Document_Storage";

            
            Guid docName = Guid.NewGuid();

            System.IO.File.WriteAllBytes(storage + "\\" + docName + ".jpg", Convert.FromBase64String(base64_file_string));
            return storage + "\\" + docName + ".jpg";
        }
    }
}
