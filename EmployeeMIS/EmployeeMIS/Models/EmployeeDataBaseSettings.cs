using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMIS.Models
{
    public class EmployeeDataBaseSettings
    {
        public String EmployeesCollectionName { get; set; }
        
        public String DepartmentsCollectionName { get; set; }

        public String ConnectionString { get; set; }

        public String DatabaseName { get; set; }
    }

    public interface IEmployeeDataBaseSettings
    {
        String EmployeesCollectionName { get; set; }
        
        String DepartmentsCollectionName { get; set; }
        
        String ConnectionString { get; set; }

        String DatabaseName { get; set; }
    }
}
