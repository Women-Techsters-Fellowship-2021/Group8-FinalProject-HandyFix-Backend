using HandyFix.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandyFix.DataAcccess
{
    public class HandyFixDBContext : IdentityDbContext<User>
    {
        public HandyFixDBContext(DbContextOptions<HandyFixDBContext> options): base(options)
        {

        }
        public HandyFixDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HandyFixDBContext>();
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=HandyFixApp;Integrated Security=true;");

                return new HandyFixDBContext(optionsBuilder.Options);
        }
        public DbSet<ResetPassword> ResetPassword { get; set; }

    }
}
