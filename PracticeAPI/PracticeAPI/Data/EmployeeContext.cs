using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeMIS.Models;

namespace PracticeAPI.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext (DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeMIS.Models.Employee> Employee { get; set; }
    }
}
