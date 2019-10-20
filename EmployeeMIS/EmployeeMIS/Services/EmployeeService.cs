using EmployeeMIS.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMIS.Services
{
    public class EmployeeService
    {
        public IMongoCollection<Employee> _employees;

        public EmployeeService(EmployeeDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            _employees = db.GetCollection<Employee>(settings.EmployeesCollectionName);
        }

        public List<Employee> Get()
        {
            return _employees.Find<Employee>(employee => true).ToList();
        }

        public Employee Get(String id)
        {
            return _employees.Find<Employee>(employee => employee.Id == id).FirstOrDefault();
        }

        public Employee Create(Employee employee)
        {
            _employees.InsertOne(employee);

            return employee;
        }

        public void Update(String id,Employee employeeInput)
        {
            _employees.ReplaceOne(employee => employee.Id == id, employeeInput);
        }

        public void Remove(String id) => _employees.DeleteOne(employee => employee.Id == id);

        public void Remove(Employee employeeInput) => _employees.DeleteOne(employee => employee.Id == employeeInput.Id);
    }
}
