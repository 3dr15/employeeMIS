using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeMIS.Models;

namespace PracticeAPI.Models
{
    public class DepartmentContext : DbContext
    {
        public DepartmentContext (DbContextOptions<DepartmentContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeMIS.Models.Department> Department { get; set; }
    }
}
