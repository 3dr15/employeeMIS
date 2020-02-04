using AutoMapper;
using PracticeAPI.DLL.Interfaces;
using PracticeAPI.BLL.Interfaces;
using PracticeAPI.BLL.Models;
using System.Collections.Generic;

namespace PracticeAPI.BLL.Logics
{
    public class DepartmentLogic : IDepartmentLogic
    {
        IDepartmentRepository _departmentRepository;
        IMapper _mapper;
        public DepartmentLogic(IDepartmentRepository department,IMapper mapper)
        {
            _departmentRepository = department;
            _mapper = mapper;
        }
        public Department CreateDepartment(Department department) => 
            _mapper.Map<Department>(_departmentRepository.CreateDepartment(_mapper.Map<DLL.Models.Department>(department)));

        public Department DeleteDepartment(int id) =>
            _mapper.Map<Department>(_departmentRepository.DeleteDepartment(id));

        public Department GetDepartment(long id) => 
            _mapper.Map<Department>(_departmentRepository.GetDepartment(id));

        public IEnumerable<Department> GetDepartments() =>
            _mapper.Map<IEnumerable<Department>>(_departmentRepository.GetDepartments());

        public Department UpdateDepartment(long id, Department department) =>
            _mapper.Map<Department>(_departmentRepository.UpdateDepartment(id, _mapper.Map<DLL.Models.Department>(department)));
    }
}
