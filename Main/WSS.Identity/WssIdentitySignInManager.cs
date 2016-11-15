using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace WSS.Identity
{
    [NotMapped]
    public class WssIdentitySignInManager : SignInManager<WssIdentityUser, string>
    {
        public WssIdentitySignInManager(WssIdentityUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(WssIdentityUser user)
        {
            return user.GenerateUserIdentityAsync((WssIdentityUserManager)UserManager);
        }

        public static WssIdentitySignInManager Create(IdentityFactoryOptions<WssIdentitySignInManager> options, IOwinContext context)
        {
            return new WssIdentitySignInManager(context.GetUserManager<WssIdentityUserManager>(), context.Authentication);
        }
    }
}