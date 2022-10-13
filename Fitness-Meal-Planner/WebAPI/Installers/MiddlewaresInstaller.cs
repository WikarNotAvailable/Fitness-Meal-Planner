using WebAPI.Middlewares;

namespace WebAPI.Installers
{
    public class MiddlewaresInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
        }
    }
}
