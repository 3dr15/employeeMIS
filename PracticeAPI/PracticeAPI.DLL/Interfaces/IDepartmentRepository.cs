﻿using PracticeAPI.DLL.Models;
using System;
using System.Collections.Generic;

namespace PracticeAPI.DLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(Int64 id);
        Department UpdateDepartment(Int64 id, Department department);
        Department CreateDepartment(Department department);
        Department DeleteDepartment(int id);
    }
}
