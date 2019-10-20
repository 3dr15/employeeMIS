using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeMIS.Models;
using EmployeeMIS.Services;

namespace EmployeeMIS.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeService _emplyeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _emplyeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            return _emplyeeService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        public ActionResult<Employee> Get(String id)
        {
            var employee = _emplyeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public ActionResult<Employee> Create(Employee employee)
        {
            _emplyeeService.Create(employee);

            return CreatedAtRoute("GetEmployee", new { id = employee.Id.ToString() }, employee);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(String id,Employee employeeInput)
        {
            var employee = _emplyeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            _emplyeeService.Update(id, employeeInput);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(String id)
        {
            var employee = _emplyeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            _emplyeeService.Remove(id);

            return NoContent();
        }

    }
}
