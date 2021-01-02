using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APITwo.Installers
{
    public class GeneralServices : IInstallers
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterEasyNetQ("host=localhost");
            services.AddTransient<IBusHub, BusHub>();
            services.AddHostedService<ConsumerService>();
            services.AddAuthorization();
        }
    }
}