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

namespace PracticeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeMISContext _context;

        public EmployeeController(EmployeeMISContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            //return await _context.Employee.ToListAsync();
            return await _context.Employee.Include(employee => employee.Department).ToListAsync();
            //return await _context.Employee.Include(employee => employee.Department).ToListAsync().ConfigureAwait(true);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            /*var employee = await _context.Employee.FindAsync(id);*/
            var employee = await _context.Employee.Include(employee => employee.Department)
                .FirstOrDefaultAsync(employee => employee.EmployeeID == id);


            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            // string storage = Environment.CurrentDirectory;
            string storage = AppDomain.CurrentDomain.BaseDirectory + "\\Document_Storage";
            Guid docName = Guid.NewGuid();
            
            System.IO.File.WriteAllBytes(storage + "\\" + docName +".jpg", Convert.FromBase64String(employee.DocProofLink));

            employee.DocProofLink = storage + "..\\" + docName + ".jpg";
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeID }, employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(long id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }

        static void FileUpload(byte[] bytes)
        {
            
        }
    }
}
