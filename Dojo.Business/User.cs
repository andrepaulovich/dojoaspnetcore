using System;
using System.Collections.Generic;

namespace Dojo.Business
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}