using AutoMapper;
using PracticeAPI.DLL.Models;
using PracticeAPI.DLL.Classes;

namespace PracticeAPI.Helper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Employee, EmployeeView>();
            CreateMap<Department, DepartmentView>();
        }
    }
}
