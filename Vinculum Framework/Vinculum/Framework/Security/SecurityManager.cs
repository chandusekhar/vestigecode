namespace Vinculum.Framework.Security
{
    using Microsoft.Practices.EnterpriseLibrary.Security;
    using System;
    using System.Security.Principal;
    using System.Web.Security;
    using Vinculum.Framework.Cryptography;

    public class SecurityManager
    {
        private ISecurityCacheProvider m_cache = SecurityCacheFactory.GetSecurityCacheProvider("Caching Store Provider");
        private IToken m_iToken;

        public bool AuthenticateUser(IToken token)
        {
            bool returnValue = false;
            try
            {
                if (string.IsNullOrEmpty(token.Value))
                {
                    throw new ArgumentNullException("token", "token can't be null or blank");
                }
                if (this.m_cache.GetIdentity(token).IsAuthenticated)
                {
                    returnValue = true;
                }
            }
            catch
            {
                throw;
            }
            return returnValue;
        }

        private IToken GenrateSecurityToken(string userId, string password)
        {
            IIdentity identity = null;
            CryptographyManager cryptographyManager = new CryptographyManager(CryptographyProviderType.SymmetricCryptoProvider);
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("userId or password", "userId or password can't be null or blank");
            }
            try
            {
                if (Membership.ValidateUser(userId, cryptographyManager.Decrypt(password)))
                {
                    identity = new GenericIdentity(userId, Membership.Provider.Name);
                }
                if (identity != null)
                {
                    this.m_iToken = this.m_cache.SaveIdentity(identity);
                }
            }
            catch
            {
                throw;
            }
            return this.m_iToken;
        }

        public bool TerminateUser(IToken token)
        {
            bool returnValue = false;
            try
            {
                this.m_cache.ExpireIdentity(token);
                returnValue = true;
            }
            catch
            {
                throw;
            }
            return returnValue;
        }

        public bool ValidateUserByFunction(string userId, string role, string task)
        {
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(userId), new string[] { role });
            return AuthorizationFactory.GetAuthorizationProvider("RuleProvider").Authorize(principal, task);
        }
    }
}

