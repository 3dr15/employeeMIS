using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeMIS.Models;
using EmployeeMIS.Services;

namespace EmployeeMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private DepartmentService _departmentService;

        public DepartmentsController(DepartmentService context)
        {
            _departmentService = context;
        }

        [HttpGet]
        public ActionResult<List<Department>> Get() => _departmentService.Get();

        [HttpGet("{id:length(24)}", Name = "GetDepartment")]
        public ActionResult<Department> Get(String id)
        {
            if (_departmentService.Get(id) == null)
            {
                return NotFound();
            }

            return _departmentService.Get(id);
        }

        [HttpPost]
        public ActionResult<Department> Create(Department department)
        {
            _departmentService.Create(department);

            return CreatedAtRoute("GetDepartment", new { id = department.Id.ToString() }, department);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(String id,Department departmentInput)
        {
            var department = _departmentService.Get(id);
            if (department == null)
            {
                return NotFound();
            }
            _departmentService.Update(id,departmentInput);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(String id)
        {
            var department = _departmentService.Get(id);
            if (department == null)
            {
                return NotFound();
            }

            _departmentService.Remove(id);

            return NoContent();
        }

    }
}
