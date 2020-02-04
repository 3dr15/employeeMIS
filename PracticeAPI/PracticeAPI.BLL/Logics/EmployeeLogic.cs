using AutoMapper;
using PracticeAPI.DLL.Interfaces;
using PracticeAPI.BLL.Interfaces;
using PracticeAPI.BLL.Models;
using System.Collections.Generic;

namespace PracticeAPI.BLL.Logics
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeLogic(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public Employee CreateEmployee(Employee employee) => 
            _mapper.Map<Employee>(_employeeRepository.CreateEmployee(_mapper.Map<DLL.Models.Employee>(employee)));

        public Employee DeleteEmployee(long id) => 
            _mapper.Map<Employee>(_employeeRepository.DeleteEmployee(id));

        public IEnumerable<Employee> FindEmployees(string searchString) => 
            _mapper.Map<IEnumerable<Employee>>(_employeeRepository.FindEmployees(searchString));

        public Employee GetEmployee(long id) => 
            _mapper.Map<Employee>(_employeeRepository.GetEmployee(id));

        public int GetEmployeeCount() => 
            _employeeRepository.GetEmployeeCount();

        public IEnumerable<Employee> GetEmployees(Pagination pagination)
        {
            PracticeAPI.DLL.Models.Pagination pagination1 = _mapper.Map<DLL.Models.Pagination>(pagination);
            IEnumerable<PracticeAPI.DLL.Models.Employee> employees = _employeeRepository.GetEmployees(pagination1);
            IEnumerable<Employee> employees1 = _mapper.Map<IEnumerable<Employee>>(employees);
            return employees1;
        }

        public Employee UpdateEmployee(long id, Employee employee) => 
            _mapper.Map<Employee>(_employeeRepository.UpdateEmployee(id, _mapper.Map<PracticeAPI.DLL.Models.Employee>(employee)));
    }
}
