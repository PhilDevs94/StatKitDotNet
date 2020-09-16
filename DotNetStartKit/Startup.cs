using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetCoreStartKit.Data;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using DotNetCoreStartKit.Core.Model;
using Microsoft.OData.Edm;
using DotNetCoreStartKit.Service;
using static DotNetCoreStartKit.Service.StudentService;
using DotNetCoreStartKit.Core.Repository;
using DotNetCoreStartKit.Core.DataContext;
using DotNetCoreStartKit.Core.UnitOfWork;

namespace DotNetStartKit
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
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc().AddControllersAsServices(); 
            services.AddControllers(mvcOptions =>
            mvcOptions.EnableEndpointRouting = false).AddControllersAsServices(); 
            services.AddOData();
            services.AddODataQueryFilter();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddScoped<IDataContext, DataContext>();
            services.AddTransient<DbContext, DataContext>();
            services.AddTransient<IUnitOfWorkAsync, UnitOfWork>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(Repository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors( options =>
            options.WithOrigins("http://localhost:4200/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            
            );
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc(routerbuilder =>
            {
                routerbuilder
                .Select()
                .Filter()
                .Expand()
                .Count()
                .MaxTop(null)
                .OrderBy();
                routerbuilder.EnableDependencyInjection();
                routerbuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        private IEdmModel GetEdmModel()
        {
            var edmBuilder = new ODataConventionModelBuilder();
            edmBuilder.EntitySet<Student>("StudentsDto");
            return edmBuilder.GetEdmModel();
        }
    }
}
