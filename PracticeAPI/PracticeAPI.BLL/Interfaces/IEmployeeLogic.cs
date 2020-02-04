using System;
using System.Collections.Generic;
using PracticeAPI.BLL.Models;

namespace PracticeAPI.BLL.Interfaces
{
    public interface IEmployeeLogic
    {
        IEnumerable<Employee> GetEmployees(Pagination pagination);
        Employee GetEmployee(Int64 id);
        int GetEmployeeCount();
        IEnumerable<Employee> FindEmployees(string searchString);
        Employee UpdateEmployee(Int64 id, Employee employee);
        Employee CreateEmployee(Employee employeeView);
        Employee DeleteEmployee(Int64 id);
    }
}
