using Microsoft.AspNet.Identity;

namespace WSS.Identity
{
    public interface IWssIdentityUserManager
    {
        WssIdentityUser FindByEmail(string email);
        WssIdentityUser FindById(string userId);
        IdentityResult ChangePassword(string userId, string currentPassword, string newPassword);
        IdentityResult Update(WssIdentityUser user);
        IdentityResult Delete(WssIdentityUser user);
        string GeneratePasswordResetToken(string v);
        object ResetPassword(string id, string code, string password);
    }
}