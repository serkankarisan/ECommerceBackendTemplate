using Core.Utilities.Helpers;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ServiceRegistration
    {
        public static void AddCoreRegistration(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFileHelperService, FileHelperManager>();
        }
    }
}
