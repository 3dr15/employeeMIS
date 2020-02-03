using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.DLL.Data;
using PracticeAPI.DLL.Interfaces;
using PracticeAPI.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPI.DLL.Classes
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private EmployeeMISContext _context;
        private readonly IMapper _IMapper;
        public DepartmentRepository(EmployeeMISContext context, IMapper mapper)
        {
            _context = context;
            _IMapper = mapper;
        }

        public IEnumerable<DepartmentView> GetDepartments()
        {
            var departments = _context.Department.ToList();
            var departmentList = _IMapper.Map<List<DepartmentView>>(departments);
            return departmentList;
        }
        public DepartmentView GetDepartment(Int64 id)
        {
            var departmentT = _context.Department.Include(department => department.Employees)
                .FirstOrDefault(department => department.DepartmentID == id);
            if (!DepartmentExists(id))
            {
                return null;
            }
            var dep = _IMapper.Map<DepartmentView>(departmentT);
            return dep;
        }
        public DepartmentView UpdateDepartment(Int64 id, DepartmentView department) // There is no Auto Mapping can be acception
        {
            // _context.Entry(_IMapper.Map<Department>(department)).State = EntityState.Modified;

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
        public DepartmentView CreateDepartment(DepartmentView departmentView)
        {
            try
            {
                var department = _IMapper.Map<Department>(departmentView);
                _context.Department.Add(department);
                _context.SaveChangesAsync();
                return _IMapper.Map<DepartmentView>(department);
            }
            catch(Exception)
            {
                return null;
            }
        }
        public DepartmentView DeleteDepartment(int id)
        {
            if (!DepartmentExists(id))
            {
                return null;
            }

            var department = _context.Department.FindAsync(id);
            _context.Department.Remove(department.Result);
            _context.SaveChangesAsync();
            return _IMapper.Map<DepartmentView>(department);
        }
        private bool DepartmentExists(Int64 id)
        {
            return _context.Department.Any(e => e.DepartmentID == id);
        }
    }
}
