using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EmployeeMIS.Models;
using EmployeeMIS.Services;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMIS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmployeeDataBaseSettings>(Configuration.GetSection(nameof(EmployeeDataBaseSettings)));

            services.AddSingleton<EmployeeDataBaseSettings>(sp =>
            sp.GetRequiredService<IOptions<EmployeeDataBaseSettings>>().Value);
            services.AddSingleton<EmployeeService>();
            services.AddControllers();

            services.AddDbContext<EmployeeMISContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EmployeeMISContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
