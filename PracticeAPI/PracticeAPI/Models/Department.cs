using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMIS.Models
{
    public class Department
    {
        public long Id { get; set; }

        public string DepartmentName { get; set; }

        // public ICollection<Employee> Employees { get; set; }
    }
}
