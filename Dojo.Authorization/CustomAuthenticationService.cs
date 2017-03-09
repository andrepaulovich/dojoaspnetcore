using Dojo.Business;
using System.Linq;

namespace Dojo.Authorization
{
    public interface ICustomAuthenticationService
    {
        bool Authenticate(string login, string password);
    }

    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        public SecurityContext SecurityContext { get; set; }

        public CustomAuthenticationService(SecurityContext securityContext)
        {
            SecurityContext = securityContext;
        }

        public bool Authenticate(string login, string password)
        {
            return SecurityContext.Users.Any(f => f.Login == login && f.Password == password);
        }
    }
}
