using PracticeAPI.DLL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPI.DLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<DepartmentView> GetDepartments();
        DepartmentView GetDepartment(Int64 id);
        DepartmentView UpdateDepartment(Int64 id, DepartmentView department);
        DepartmentView CreateDepartment(DepartmentView departmentView);
        DepartmentView DeleteDepartment(int id);
    }
}
