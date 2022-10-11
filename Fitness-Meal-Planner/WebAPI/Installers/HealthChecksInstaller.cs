using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebAPI.Installers
{
    //installer responsible for configuring health checks
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("FitnessPlannerCS"), tags: new[] { "database" });
            builder.Services.AddHealthChecksUI().AddInMemoryStorage();
        }
    }
}
