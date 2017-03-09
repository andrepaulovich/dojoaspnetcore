using Microsoft.AspNetCore.Builder;

namespace Dojo.Authorization
{
    public static class SimpleAuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder UseSimpleAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleAuthenticationMiddleware>();
        }
    }
}
