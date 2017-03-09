using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dojo.Business
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<UserRole> UserRoles { get; set; }
    }
}