using HandyFix.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyFix.DataAcccess
{
    public class Seeder
    {
        public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, HandyFixDBContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Regular"};

                foreach (var role in roles)
                {
                  await  roleManager.CreateAsync(new IdentityRole { Name = role });
                }
               
                List<User> users = new List<User>
                {
                    new User
                    {
                        Email = "JohnJack@gmail.com",
                        UserName = "Jonnie"
                    },
                     new User
                    {
                        Email = "AnnePaul@gmail.com",
                        UserName = "Annie"
                    }
                };


                foreach (var user in users)
                {
                   await userManager.CreateAsync(user, "P@ssW0rd");
                   if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                        await userManager.AddToRoleAsync(user, "Regular");
                    }

                }
            }
        }
    }
}
