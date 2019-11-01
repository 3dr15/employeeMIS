using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeMIS.Models;

namespace PracticeAPI.Models
{
    public class EmployeeMISContext : DbContext
    {
        public EmployeeMISContext (DbContextOptions<EmployeeMISContext> options)
            : base(options)
        {
        }
        
        public DbSet<EmployeeMIS.Models.Employee> Employee { get; set; }
        public DbSet<EmployeeMIS.Models.Department> Department { get; set; }
    }
}
