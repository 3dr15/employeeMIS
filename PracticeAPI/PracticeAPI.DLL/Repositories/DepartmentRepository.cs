using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.DLL.Data;
using PracticeAPI.DLL.Interfaces;
using PracticeAPI.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeAPI.DLL.Classes
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private EmployeeMISContext _context;
        // private readonly IMapper _IMapper;
        public DepartmentRepository(EmployeeMISContext context, IMapper mapper)
        {
            _context = context;
            // _IMapper = mapper;
        }

        public IEnumerable<Department> GetDepartments()
        {
            var departments = _context.Department.ToList();
            // var departmentList = _IMapper.Map<List<Department>>(departments);
            return departments;
        }
        public Department GetDepartment(Int64 id)
        {
            var departmentT = _context.Department.Include(department => department.Employees)
                .FirstOrDefault(department => department.DepartmentID == id);
            if (!DepartmentExists(id))
            {
                return null;
            }
            // var dep = _IMapper.Map<Department>(departmentT);
            return departmentT;
        }
        public Department UpdateDepartment(Int64 id, Department department) // There is no Auto Mapping can be acception
        {
            _context.Entry(department).State = EntityState.Modified;
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return department;
        }
        public Department CreateDepartment(Department department)
        {
            try
            {
                _context.Department.Add(department);
                _context.SaveChangesAsync();
                return department;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public Department DeleteDepartment(int id)
        {
            if (!DepartmentExists(id))
            {
                return null;
            }

            var department = _context.Department.FindAsync(id);
            _context.Department.Remove(department.Result);
            _context.SaveChangesAsync();
            return department.Result;
        }
        private bool DepartmentExists(Int64 id)
        {
            return _context.Department.Any(e => e.DepartmentID == id);
        }
    }
}
