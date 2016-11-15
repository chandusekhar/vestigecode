using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WSS.Identity
{
    public partial class WssIdentityContext : IdentityDbContext<WssIdentityUser>
    {
        public WssIdentityContext() : base("WssIdentity")
        {
            Database.SetInitializer<WssIdentityContext>(null);
        }

        public static WssIdentityContext Create()
        {
            return new WssIdentityContext();
        }
    }
}
