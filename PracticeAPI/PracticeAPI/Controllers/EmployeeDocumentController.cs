using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using PracticeAPI.DLL.Data;

namespace PracticeAPI.Controllers
{
    // [EnableCors("_myAllowSpecificOrigins")]
    // [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [EnableCorsAttribute("_myAllowSpecificOrigins")]
    [ApiController]
    public class EmployeeDocumentController : ControllerBase
    {
        private readonly EmployeeMISContext _context;

        public EmployeeDocumentController(EmployeeMISContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeDocument(long id)
        {
            var employee = _context.Employee.FindAsync(id).Result;
            
            if (employee == null)
            {
                return NotFound();
            }
            byte[] bytes = System.IO.File.ReadAllBytes(employee.DocProofLink);
            return File(bytes,"image/jpg");
        }
    }
}