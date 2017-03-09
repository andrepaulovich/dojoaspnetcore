using Microsoft.Extensions.DependencyInjection;

namespace Dojo.Authorization
{
    public static class CustomAuthenticationServiceExtension
    {

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddTransient<ICustomAuthenticationService, CustomAuthenticationService>();
            return services;
        }

    }
}
