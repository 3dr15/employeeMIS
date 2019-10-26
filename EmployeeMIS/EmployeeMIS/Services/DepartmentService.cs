using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMIS.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace EmployeeMIS.Services
{
    public class DepartmentService
    {
        public IMongoCollection<Department> _departments;
        public DepartmentService(EmployeeDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            _departments = db.GetCollection<Department>(settings.DepartmentsCollectionName);
        }

        /*public DepartmentService(DbContextOptions<EmployeeMISContext> options)
            : base(options)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("EmployeeDataBase");

            _departments = db.GetCollection<Department>("Department");
        }*/

        public List<Department> Get() => 
            _departments.Find<Department>(department => true).ToList();

        public Department Get(String id) => 
            _departments.Find<Department>(department => department.Id == id).FirstOrDefault();

        public Department Create(Department department)
        {
            _departments.InsertOne(department);
            return department;
        }

        public void Update(String id,Department department)
        {
            _departments.ReplaceOne(dep => dep.Id == id, department);
        }

        public void Remove(String id)
        {
            _departments.DeleteOne(dep => dep.Id == id.ToString());
        }

        public void Remove(Department department)
        {
            _departments.DeleteOne(dep => dep.Id == department.Id.ToString());
        }

        public DbSet<EmployeeMIS.Models.Department> Department { get; set; }
    }
}
