using Components.Irsa;
using Irsa.Components.Soap;
using Irsa.Repository.Wrapper;
using Microsoft.Extensions.DependencyInjection;


namespace Irsa.Configs
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IServiceIrsa, ServiceIrsa>();
            services.AddScoped<IXmlService, XmlService>();
            services.AddScoped<IManualLog, ManualLog>();
            return services;
        }
    }
}
