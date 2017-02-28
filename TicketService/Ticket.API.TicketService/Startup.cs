using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.SwaggerGen.Generator;
using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.Swagger.Model;
using Ticket.API.Common;
using Ticket.DataAccess.Common;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Ticket.Base.Repositories;
using Ticket.BusinessLogic.Security;
using Ticket.DataAccess.TicketService;
using Ticket.BusinessLogic.TicketService;

namespace Molecular.API.TicketService
{

    public class Startup
    {


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //connection = Configuration["connectionString"];
            //Console.WriteLine(connection);

            /* common has IConfiguration,HttpContextAccessor,DatabaseContext,ValidationErrorCodes,CORS*/
            services.AddCommonService(Configuration);
            services.AddSingleton<BaseValidationErrorCodes, Ticket.DataAccess.Security.ValidationErrorCodes>();

            services.AddSwaggerService();
            services.AddAssetServiceRepositories();
            services.AddSingleton<Ticket.BusinessLogic.Common.EncryptionProvider>();
            services.AddScoped<IUserRepository, UserRepository<Ticket.DataAccess.Security.User , Ticket.DataAccess.Security.Role ,Ticket.DataAccess.TicketService.Client>>();
            services.AddScoped<IGroupUserRepository, GroupUserRepository<Ticket.DataAccess.Security.GroupUser>>();
            services.AddScoped<IRoleRepository, RoleRepository<Ticket.DataAccess.Security.Role>>();
            services.AddCustomMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseCors(builder =>
               builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               );

            app.UseSwagger();
            app.UseSwaggerUi();

            app.UseJwtAuthentication();

            app.UseMvc();
        }
    }

}