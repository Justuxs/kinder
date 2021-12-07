using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kinder_app.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string UserUsername { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Karma_points { get; set; }
    }
}