﻿using PracticeAPI.DLL.Models;
using System;
using System.Collections.Generic;

namespace PracticeAPI.DLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Pagination pagination);
        Employee GetEmployee(Int64 id);
        int GetEmployeeCount();
        IEnumerable<Employee> FindEmployees(string searchString);
        Employee UpdateEmployee(Int64 id, Employee employee);
        Employee CreateEmployee(Employee employee);
        Employee DeleteEmployee(Int64 id);
    }
}
