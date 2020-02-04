using System;
using System.Collections.Generic;
using PracticeAPI.Helper.Models;

namespace PracticeAPI.Helper.Interfaces
{
    public interface IDepartmentTask
    {
        IEnumerable<Department> Departments { get; }

        Department GetDepartment(Int64 id);
        Department UpdateDepartment(Int64 id, Department department);
        Department CreateDepartment(Department department);
        Department DeleteDepartment(int id);
    }
}
