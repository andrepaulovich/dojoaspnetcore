using Newtonsoft.Json;
using System;

namespace Dojo.Business
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }
    }
}