using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Installers
{
    public static class InstallerExtensions
    {
        // class responsible for registration of services using each installer
        public static void InstallServicesInAssembly(this WebApplicationBuilder builder)
        {
            var installers = typeof(Program).Assembly.ExportedTypes.Where(x =>
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(builder));
        }
    }
}
