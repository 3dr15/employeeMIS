using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PracticeAPI.DLL.Data
{
    public class EmployeeMISContext : DbContext
    {
        public EmployeeMISContext (DbContextOptions<EmployeeMISContext> options)
            : base(options)
        {
        }
        
        public DbSet<PracticeAPI.DLL.Models.Employee> Employee { get; set; }
        public DbSet<PracticeAPI.DLL.Models.Department> Department { get; set; }
    }
}
