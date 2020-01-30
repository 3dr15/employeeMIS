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

            /*
            if(Configuration["Server"] == null)
            {
                services.AddDbContext<EmployeeMISContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("EmployeeMISContext")));
            }
            else
            {
                var server = Configuration["DBServer"];
                var port = Configuration["DBPort"];
                var user = Configuration["DBUser"];
                var password = Configuration["DBPassword"];
                var database = Configuration["Database"];

                services.AddDbContext<EmployeeMISContext>(options =>
                        options.UseSqlServer($"Server={server},{port};Initial Catalog={database}; User ID={user};Password={password}"));
            }
            */

            services.AddDbContext<EmployeeMISContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("EmployeeMISContext")));    // Uncomment Me


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

            
            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); // This should be called only after useRouting() and before UseAuthentication()

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // PrepDB.PrepPopulation(app); // Comment This
        }
    }
}
