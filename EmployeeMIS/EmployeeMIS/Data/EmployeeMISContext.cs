using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMIS.Models
{
    public class EmployeeMISContext : DbContext
    {
        public EmployeeMISContext (DbContextOptions<EmployeeMISContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeMIS.Models.Employee> Employee { get; set; }
    }
}
