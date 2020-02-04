using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.DLL.Data;
using PracticeAPI.DLL.Classes;
using PracticeAPI.DLL.Interfaces;
using AutoMapper;
using PracticeAPI.Helper;
using PracticeAPI.Helper.Interfaces;
using PracticeAPI.Helper.TaskHandler;
using PracticeAPI.BLL.Interfaces;
using PracticeAPI.BLL.Logics;

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

            // Dependency Injections
            // For DB Context            
            services.AddDbContext<EmployeeMISContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("EmployeeMISContext")));    // Uncomment Me
            
            // FOR CONTROLLERS
            services.AddScoped<IDepartmentTask, DepartmentTask>();
            services.AddScoped<IEmployeeTask, EmployeeTask>();

            // FOR TASKS
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // FOR LOGIC
            services.AddScoped<IDepartmentLogic, DepartmentLogic>();
            services.AddScoped<IEmployeeLogic, EmployeeLogic>();

            services.AddControllers();
            services.AddMvc(opt => opt.EnableEndpointRouting=false);
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

            services.AddCors(opt =>
            {
                opt.AddPolicy(MyAllowSpecificOrigins, builder =>
                    builder.WithOrigins("http://localhost:4200", "http://localhost:8888")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });
            services.AddAutoMapper(typeof(AutoMapping));
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

            app.UseCors(MyAllowSpecificOrigins); // This should be called only after useRouting() and before UseAuthentication()

            app.UseAuthorization();
            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(MyAllowSpecificOrigins);
                // endpoints.MapControllers();
            });

            // PrepDB.PrepPopulation(app); // Comment This
        }
    }
}
