using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPI.DLL.Models
{
    public class Employee
    {
        public long EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long PhoneNumber { get; set; }
        
        public string DocProofLink { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        
        public long Salary { get; set; }

    }
}
