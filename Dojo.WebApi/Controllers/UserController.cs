using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dojo.Business;
using Microsoft.EntityFrameworkCore;

namespace Dojo.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public SecurityContext SecurityContext { get; set; }

        public UserController(SecurityContext securityContext)
        {
            SecurityContext = securityContext;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var list = SecurityContext.Users.Include("UserRoles.Role").ToList();
            return Ok(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            var user = SecurityContext.Users.Include("UserRoles.Role").Where(f => f.Id == id);
            return "value";
        }        
    }
}
