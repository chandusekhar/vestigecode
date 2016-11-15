using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WSS.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WSS.Identity.WssIdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WSS.Identity.WssIdentityContext context)
        {
            //var passwordHasher = new PasswordHasher();

            //context.Users.AddOrUpdate(new WssIdentityUser
            //{
            //    UserName = "dfardoe@winnipeg.ca",
            //    PasswordHash = passwordHasher.HashPassword("Pa$$w0rd"),
            //    SecurityStamp = Guid.NewGuid().ToString()
            //});

            var store = new UserStore<WssIdentityUser>(context);
            var manager = new WssIdentityUserManager(store);
            var user = context.Users.FirstOrDefault(u => u.Id == "7226679b-2d05-41a6-bbea-b45f7773f300");
            if (null == user)
            {
                user = new WssIdentityUser
                {
                    Id = "7226679b-2d05-41a6-bbea-b45f7773f300",
                    UserName = "nsmith@gmail.com",
                    Email = "nsmith@gmail.com",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                manager.Create(user, "passw0rd");
            }
            else
            {
                user.Id = "7226679b-2d05-41a6-bbea-b45f7773f300";
                user.UserName = "nsmith@gmail.com";
                user.Email = "nsmith@gmail.com";
                user.EmailConfirmed = true;
                user.SecurityStamp = Guid.NewGuid().ToString();
                manager.Update(user);
            }

            user = context.Users.FirstOrDefault(u => u.Id == "7c07e6ef-8cad-4c0e-be59-1c5701c81909");
            if (null == user)
            {
                user = new WssIdentityUser
                {
                    Id = "7c07e6ef-8cad-4c0e-be59-1c5701c81909",
                    UserName = "ajarvis@yahoo.com",
                    Email = "ajarvis@yahoo.com",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                manager.Create(user, "passw0rd");
            }
            else
            {
                user.Id = "7c07e6ef-8cad-4c0e-be59-1c5701c81909";
                user.UserName = "ajarvis@yahoo.com";
                user.Email = "ajarvis@yahoo.com";
                user.EmailConfirmed = true;
                user.SecurityStamp = Guid.NewGuid().ToString();
                manager.Update(user);
            }

            user = context.Users.FirstOrDefault(u => u.Id == "ecab06c0-3494-41e3-95fc-63ed1496eddd");
            if (null == user)
            {
                user = new WssIdentityUser
                {
                    Id = "ecab06c0-3494-41e3-95fc-63ed1496eddd",
                    UserName = "lashley@yahoo.com",
                    Email = "lashley@yahoo.com",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                manager.Create(user, "passw0rd");
            }
            else
            {
                user.Id = "ecab06c0-3494-41e3-95fc-63ed1496eddd";
                user.UserName = "lashley@yahoo.com";
                user.Email = "lashley@yahoo.com";
                user.EmailConfirmed = true;
                user.SecurityStamp = Guid.NewGuid().ToString();
                manager.Update(user);
            }
        }
    }
}
