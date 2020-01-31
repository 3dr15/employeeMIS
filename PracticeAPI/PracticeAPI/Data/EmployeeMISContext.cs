﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models;

namespace PracticeAPI.Models
{
    public class EmployeeMISContext : DbContext
    {
        public EmployeeMISContext (DbContextOptions<EmployeeMISContext> options)
            : base(options)
        {
        }
        
        public DbSet<PracticeAPI.Models.Employee> Employee { get; set; }
        public DbSet<PracticeAPI.Models.Department> Department { get; set; }
    }
}
