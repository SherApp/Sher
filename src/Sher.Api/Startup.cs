using System;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sher.Api.Utils;
using Sher.Application.Access.InitializePlatform;
using Sher.Application.Access.Jwt;
using Sher.Infrastructure;
using Sher.Infrastructure.Crypto;
using Sher.Infrastructure.Tus;
using tusdotnet;

namespace Sher.Api
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
            services.AddDbContext(Configuration.GetConnectionString("Default"));
            
            services.Configure<PasswordHashingOptions>(Configuration.GetSection("PasswordHashingOptions"));
            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));

            services.AddJwt(Configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddControllers(cfg => cfg.AllowEmptyInputInBodyModelBinding = true);
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Sher.Api", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sher.Api v1"));
                app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });
            }

            app.UseRouting();

            app.UseCors(opt => opt.AllowAnyMethod());

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = GlobalExceptionHandler.Delegate
            });

            app.UseTus(tusHttpContext => tusHttpContext.SetupTus());
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            using var scope = app.ApplicationServices.CreateScope();
            
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            
            mediator.Send(new InitializePlatformCommand(Guid.NewGuid(), Configuration["Admin:EmailAddress"],
                Configuration["Admin:Password"])).Wait();
        }
    }
}