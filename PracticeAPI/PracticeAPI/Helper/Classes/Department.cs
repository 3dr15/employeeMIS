using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPI.Helper.Classes
{
    public class Department
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
