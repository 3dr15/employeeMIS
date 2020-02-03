using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.DLL.Models;

namespace PracticeAPI.DLL.Data
{
    public class EmployeeMISContext : DbContext
    {
        public EmployeeMISContext (DbContextOptions<EmployeeMISContext> options)
            : base(options)
        {
        }
        
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
