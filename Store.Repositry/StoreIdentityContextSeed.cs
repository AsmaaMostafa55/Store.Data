using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any())
            {
                var user = new AppUser { 
                
                DisplayName="AsmaaMostafa",
                Email="asmaamostafa.com",
                UserName= "AsmaaMostafa",
                Address=new Address { 
                FirstName="Asmaa",
                LastName="Mostafa",
                City="ELmahalla",
                State="Cairo",
                    Street = "3",
                    PostalCode="123456"
                }
                };
                await userManager.CreateAsync(user,"Password123");
            }
        }
    }
}
