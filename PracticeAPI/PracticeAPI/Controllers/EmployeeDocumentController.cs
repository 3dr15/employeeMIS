using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models;
using PracticeAPI.Classes;
using Microsoft.AspNetCore.Cors;

namespace PracticeAPI.Controllers
{
    // [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
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