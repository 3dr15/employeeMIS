using AutoMapper;
using PracticeAPI.BLL.Interfaces;
using PracticeAPI.Helper.Interfaces;
using PracticeAPI.Helper.Models;
using System.Collections.Generic;

namespace PracticeAPI.Helper.TaskHandler
{
    public class DepartmentTask : IDepartmentTask
    {
        IDepartmentLogic _depLogic;
        IMapper _mapper;
        public DepartmentTask(IDepartmentLogic department,IMapper mapper)
        {
            _depLogic = department;
            _mapper = mapper;
        }
        public Department CreateDepartment(Department department) => 
            _mapper.Map<Department>(_depLogic.CreateDepartment(_mapper.Map<BLL.Models.Department>(department)));

        public Department DeleteDepartment(int id) =>
            _mapper.Map<Department>(_depLogic.DeleteDepartment(id));

        public Department GetDepartment(long id) => 
            _mapper.Map<Department>(_depLogic.GetDepartment(id));

        public IEnumerable<Department> Departments =>
            _mapper.Map<IEnumerable<Department>>(_depLogic.GetDepartments());

        public Department UpdateDepartment(long id, Department department) =>
            _mapper.Map<Department>(_depLogic.UpdateDepartment(id, _mapper.Map<BLL.Models.Department>(department)));
    }
}
