using PracticeAPI.DLL.Data;
using PracticeAPI.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.DLL.Models;
using AutoMapper;

namespace PracticeAPI.DLL.Classes
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeMISContext _context;
        // private readonly IMapper _mapper;
        public EmployeeRepository(EmployeeMISContext context, IMapper mapper)
        {
            _context = context;
            // _mapper = mapper;
        }
        public int GetEmployeeCount()
        {
            int count = _context.Employee.ToList().Count();
            double numberOfPages = count / 10.0;

            return (int)Math.Ceiling(numberOfPages);
            // return (int)Math.Round(numberOfPages, 0, MidpointRounding.AwayFromZero);
        }
        public IEnumerable<Employee> GetEmployees(Pagination pagination)
        {
            var employees = _context.Employee.Include(employee => employee.Department)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync().Result;
            // var employeesList = _mapper.Map<IList<Employee>>(employees);
            return employees;
        }
        public Employee GetEmployee(Int64 id)
        {
            var employee = _context.Employee.Include(employee => employee.Department)
                .FirstOrDefaultAsync(employee => employee.EmployeeID == id).Result;
            return employee;
        }
        public IEnumerable<Employee> FindEmployees(string searchString)
        {
            var emp = from employees in _context.Employee.Include(employee => employee.Department) select employees;

            if (!string.IsNullOrEmpty(searchString))
            {
                var employees = emp.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));

                if (!employees.Any())
                {
                    return null;
                }

                return employees;
            }
            else
            {
                return null;
            }
        }
        public Employee UpdateEmployee(Int64 id, Employee employee)
        {
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
                    return null;
                }
                else
                {
                    throw;
                }
            }

            // return NoContent();
            return employee;
        }
        public Employee CreateEmployee(Employee employee)
        {
            try
            {
                employee.DocProofLink = this.FileUpload(employee.DocProofLink);
                _context.Employee.Add(employee);
                _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Employee DeleteEmployee(Int64 id)
        {
            var employee = _context.Employee.Find(id);
            if (!EmployeeExists(id))
            {
                return null;
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
            if (!System.IO.Directory.Exists(path: storage))
            {
                System.IO.Directory.CreateDirectory(storage);
            }

            Guid docName = Guid.NewGuid();

            System.IO.File.WriteAllBytes(storage + "\\" + docName + ".jpg", Convert.FromBase64String(base64_file_string));
            return storage + "\\" + docName + ".jpg";
        }

    }
}
