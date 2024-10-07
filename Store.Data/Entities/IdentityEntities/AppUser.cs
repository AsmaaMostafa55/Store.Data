using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.IdentityEntities
{
    public class AppUser:IdentityUser
    {
        public String DisplayName { get; set; }
        public Address Address { get; set; }

    }
}
