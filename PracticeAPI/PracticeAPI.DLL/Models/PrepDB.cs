using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using PracticeAPI.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace PracticeAPI.DLL.Models
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScop = app.ApplicationServices.CreateScope())
            {
                seedData(serviceScop.ServiceProvider.GetService<EmployeeMISContext>());
            }
        }

        public static void seedData(EmployeeMISContext context)
        {
            System.Console.WriteLine("Appling Migrations.....");

            context.Database.Migrate();

            if (!context.Department.Any())
            {
                System.Console.WriteLine("Seeding Data.......");

                context.Department.AddRange(
                    new Department() { DepartmentName = "Administrator" },
                    new Department() { DepartmentName = "Management" },
                    new Department() { DepartmentName = "Development" },
                    new Department() { DepartmentName = "Designing" },
                    new Department() { DepartmentName = "Testing" }
                );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Data Already Exists - Not Seeding");
            }
        }
    }
}
