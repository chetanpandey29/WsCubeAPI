using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WS_Cube.Factory;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using VMD.RESTApiResponseWrapper.Core.Extensions;
using WS_Cube.Repository.Interface;
using WS_Cube.Repository.Repositories;
using WS_Cube.Services;
using WS_Cube.Repository.Infrastructure;

namespace WS_CubeAPI
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);;
            services.AddMvc();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // Inject IDbConnection, with implementation from SqlConnection class.
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(appSettings.ServerConnection));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<IScriptEngine, ScriptEngine>();

            // configure DI for application services
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserRepository, Userepository>();

            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<ISiteService, SiteService>();

            services.AddTransient<ISiteRepository, SiteRepository>();

            services.AddTransient<IAreaService, AreaService>();

            services.AddTransient<IAreaRepository, AreaRepository>();

            services.AddTransient<ILanguageService, LanguageService>();

            services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<IGroupService, GroupService>();

            services.AddTransient<IGroupRepository, GroupRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseDeveloperExceptionPage();
            app.UseAPIResponseWrapperMiddleware();            
            app.UseMvc();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
        }
    }
}
