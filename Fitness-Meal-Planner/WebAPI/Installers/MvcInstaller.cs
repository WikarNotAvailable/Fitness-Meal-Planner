using Application;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        // installer responsible for registration of services connected with layer of application and infrastructure
        public void InstallServices(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddControllers()
                .AddOData(options => options.EnableQueryFeatures());           

            builder.Services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            builder.Services.AddMvc();
            builder.Services.AddInfrastructure();
            builder.Services.AddApplication();
        }
    }
}
