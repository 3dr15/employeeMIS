using PracticeAPI.DLL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPI.DLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeView> GetEmployees(Pagination pagination);
        EmployeeView GetEmployee(Int64 id);
        int GetEmployeeCount();
        IEnumerable<EmployeeView> FindEmployees(string searchString);
        EmployeeView UpdateEmployee(Int64 id, EmployeeView employeeView);
        EmployeeView CreateEmployee(EmployeeView employeeView);
        EmployeeView DeleteEmployee(Int64 id);
    }
}
