
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
    }
}
