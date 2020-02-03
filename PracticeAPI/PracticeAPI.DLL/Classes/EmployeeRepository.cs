using PracticeAPI.DLL.Data;
using PracticeAPI.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.DLL.Models;
using AutoMapper;

namespace PracticeAPI.DLL.Classes
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeMISContext _context;
        private readonly IMapper _mapper;
        public EmployeeRepository(EmployeeMISContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int GetEmployeeCount()
        {
            int count = _context.Employee.ToList().Count();
            double numberOfPages = count / 10.0;

            return (int)Math.Round(numberOfPages, 0, MidpointRounding.AwayFromZero);
        }
        public IEnumerable<EmployeeView> GetEmployees(Pagination pagination)
        {
            var employees = _context.Employee.Include(employee => employee.Department)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync().Result;
            var employeesList = _mapper.Map<IList<EmployeeView>>(employees);
            return employeesList;
        }
        public EmployeeView GetEmployee(Int64 id)
        {
            var employee = _context.Employee.Include(employee => employee.Department)
                .FirstOrDefaultAsync(employee => employee.EmployeeID == id).Result;
            return _mapper.Map<EmployeeView>(employee);
        }
        public IEnumerable<EmployeeView> FindEmployees(string searchString)
        {
            var emp = from employees in _context.Employee.Include(employee => employee.Department) select employees;

            if (!string.IsNullOrEmpty(searchString))
            {
                var employees = emp.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));

                if (!employees.Any())
                {
                    return null;
                }

                return _mapper.Map<IEnumerable<EmployeeView>>(employees.ToList());
            }
            else
            {
                return null;
            }
        }
        public EmployeeView UpdateEmployee(Int64 id, EmployeeView employee)
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
        public EmployeeView CreateEmployee(EmployeeView employee)
        {
            try
            {
                employee.DocProofLink = this.FileUpload(employee.DocProofLink);
                var newEmployee = _context.Employee.Add(_mapper.Map<Employee>(employee));
                _context.SaveChangesAsync();
                return _mapper.Map<EmployeeView>(newEmployee);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public EmployeeView DeleteEmployee(Int64 id)
        {
            var employee = _context.Employee.FindAsync(id).Result;
            if (!EmployeeExists(id))
            {
                return null;
            }
            _context.Employee.Remove(employee);
            _context.SaveChangesAsync();

            return _mapper.Map<EmployeeView>(employee);
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
