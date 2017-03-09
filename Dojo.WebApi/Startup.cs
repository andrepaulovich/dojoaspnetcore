using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dojo.Business;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using Dojo.Authorization;

namespace Dojo.WebApi
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
            services.AddDbContext<SecurityContext>(opt => opt.UseInMemoryDatabase());

            services.AddCustomAuthentication();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Dojo API", Version = "v1" });
            });

            // Add framework services.
            services.AddMvc();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ICustomAuthenticationService customAuthenticationService)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var db = app.ApplicationServices.GetService<SecurityContext>();
            CreateData(db);

            app.UseSimpleAuthentication();

            app.UseMvcWithDefaultRoute();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dojo API V1");
            });
        }

        #region CreateData

        private void CreateData(SecurityContext context)
        {
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Login = "joao",
                Password = "biscoito"
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                Login = "maria",
                Password = "recheado"
            };

            context.Users.Add(user1);
            context.Users.Add(user2);

            var role1 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "admin"
            };

            var role2 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "read"
            };

            context.Roles.Add(role1);
            context.Roles.Add(role2);

            var userrole1 = new UserRole
            {
                RoleId = role1.Id,
                UserId = user1.Id
            };

            var userrole2 = new UserRole
            {
                RoleId = role2.Id,
                UserId = user1.Id
            };

            var userrole3 = new UserRole
            {
                RoleId = role2.Id,
                UserId = user2.Id
            };

            context.UserRoles.Add(userrole1);
            context.UserRoles.Add(userrole2);
            context.UserRoles.Add(userrole3);

            context.SaveChanges();
        }

        #endregion

    }
}
