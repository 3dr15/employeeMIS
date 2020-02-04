namespace PracticeAPI.BLL.Models
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
