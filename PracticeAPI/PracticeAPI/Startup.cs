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
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models;
using Microsoft.Extensions.Options;

namespace PracticeAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // To allow Cors
            services.AddCors(opt =>
            {
                opt.AddPolicy(MyAllowSpecificOrigins, builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });

            /* services.AddCors(Cors => {
                 Cors.AddPolicy(MyAllowSpecificOrigins, opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
             });*/

            /*services.AddCors(Options =>
            {
                Options.AddPolicy(MyAllowSpecificOrigins, builder => 
                builder.WithOrigins("http://localhost:4200/",
                    "http://localhost:4230/",
                    "https://api.employeemis.com/EmpApp/",
                    "http://{public IP}:{public port}/",
                    "http://{public DNS name}:{public port}/")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });*/

            /*services.AddDbContext<TodoContext>(opt =>
              opt.UseInMemoryDatabase("Employee_MIS_DB"));
            */
            services.AddDbContext<EmployeeMISContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EmployeeMISContext")));

            //services.AddControllers();
            /*
             To solve Object cycle in JSON refered this link
             https://github.com/dotnet/corefx/issues/41288#issuecomment-534648397
            //services.AddControllers().AddJsonOptions(Options => Options.JsonSerializerOptions.MaxDepth=169);
             */

            /*
             To solve Object cycle in JSON refered this link
             https://stackoverflow.com/questions/57912012/net-core-3-upgrade-cors-and-jsoncycle-xmlhttprequest-error
             */
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             app.UseCors();
            // app.UseCors(MyAllowSpecificOrigins);
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
