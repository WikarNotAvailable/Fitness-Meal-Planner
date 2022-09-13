using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        // installer responsible for choosing specific sql database using DbContext class and connection string
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<FitnessPlannerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FitnessPlannerCS")));
        }
    }
}
