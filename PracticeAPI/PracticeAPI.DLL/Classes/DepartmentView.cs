using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeAPI.DLL.Classes
{
    public class DepartmentView
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public List<EmployeeView> Employees { get; set; }

    }
}
