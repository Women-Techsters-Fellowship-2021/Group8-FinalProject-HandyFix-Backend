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
        public async static Task Seed(UserManager<User> userManager, HandyFixDBContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Users.Any())
            {
               
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

                }
            }
        }
    }
}
