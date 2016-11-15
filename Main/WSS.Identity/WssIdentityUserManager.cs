using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace WSS.Identity
{
    [NotMapped]
    public class WssIdentityUserManager : UserManager<WssIdentityUser>, IWssIdentityUserManager
    {
        public WssIdentityUserManager(IUserStore<WssIdentityUser> store)
            : base(store)
        {
        }

        public static WssIdentityUserManager Create(IdentityFactoryOptions<WssIdentityUserManager> options,
            IOwinContext context)
        {
            var provider = new DpapiDataProtectionProvider("wss");

            var manager = new WssIdentityUserManager(new UserStore<WssIdentityUser>(context.Get<WssIdentityContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<WssIdentityUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.UserTokenProvider =
                new DataProtectorTokenProvider<WssIdentityUser, string>(provider.Create("UserToken"));
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 7,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false
            };

            // This is required to generate/validate the key used during password resets (by default the SecurityStamp)
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<WssIdentityUser>(dataProtectionProvider.Create("ResetPassword"));
            }

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }

        public WssIdentityUser FindByEmail(string email)
        {
            return UserManagerExtensions.FindByEmail(this, email);
        }

        public WssIdentityUser FindById(string userId)
        {
            return UserManagerExtensions.FindById(this, userId);
        }

        public IdentityResult ChangePassword(string userId, string currentPassword, string newPassword)
        {
            return UserManagerExtensions.ChangePassword(this, userId, currentPassword, newPassword);
        }

        public IdentityResult Update(WssIdentityUser user)
        {
            return UserManagerExtensions.Update(this, user);
        }

        public IdentityResult Delete(WssIdentityUser user)
        {
            return UserManagerExtensions.Delete(this, user);
        }

        public string GeneratePasswordResetToken(string v)
        {
            return UserManagerExtensions.GeneratePasswordResetToken(this, v);
        }

        public object ResetPassword(string id, string code, string password)
        {
            return UserManagerExtensions.ResetPassword(this, id, code, password);
        }
    }
}