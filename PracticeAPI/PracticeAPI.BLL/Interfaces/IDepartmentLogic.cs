using System;
using System.Collections.Generic;
using PracticeAPI.BLL.Models;

namespace PracticeAPI.BLL.Interfaces
{
    public interface IDepartmentLogic
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(Int64 id);
        Department UpdateDepartment(Int64 id, Department department);
        Department CreateDepartment(Department department);
        Department DeleteDepartment(int id);
    }
}
