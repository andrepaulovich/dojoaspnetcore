using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dojo.Authorization
{
    public class SimpleAuthenticationMiddleware
    {
        private ICustomAuthenticationService _authenticationService;
        private RequestDelegate _next;

        public SimpleAuthenticationMiddleware(RequestDelegate next, ICustomAuthenticationService authenticationService)
        {
            _next = next;
            _authenticationService = authenticationService;
        }

        public async Task Invoke(HttpContext context)
        {
            var login = context.Request.Headers["login"];
            var pwd = context.Request.Headers["password"];

            var isValid = _authenticationService.Authenticate(login, pwd);

            if (!isValid)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Aqui não queridinho!");
            }
                
            await _next.Invoke(context);
        }
    }
}
