namespace WebAPI.Installers
{
    public interface IInstaller
    {
        // installers' interface that is implemented by them and used to registration of services
        void InstallServices(WebApplicationBuilder builder);
    }
}
