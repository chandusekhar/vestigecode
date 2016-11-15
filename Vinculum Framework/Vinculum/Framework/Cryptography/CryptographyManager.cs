namespace Vinculum.Framework.Cryptography
{
    using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
    using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Configuration;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Security.Cryptography;

    public class CryptographyManager
    {
        private string m_cryptKey;
        private CryptographyProviderType m_cryptProviderType;

        public CryptographyManager(CryptographyProviderType providerType)
        {
            switch (providerType)
            {
                case CryptographyProviderType.HashProvider:
                    this.m_cryptKey = (ConfigurationManager.GetSection("securityCryptographyConfiguration") as CryptographySettings).DefaultHashProviderName;
                    break;

                case CryptographyProviderType.SymmetricCryptoProvider:
                    this.m_cryptKey = (ConfigurationManager.GetSection("securityCryptographyConfiguration") as CryptographySettings).DefaultSymmetricCryptoProviderName;
                    break;
            }
            this.m_cryptProviderType = providerType;
        }

        public CryptographyManager(CryptographyProviderType providerType, string cryptKey)
        {
            this.m_cryptKey = cryptKey;
            this.m_cryptProviderType = providerType;
        }

        public bool CompareHash(byte[] plainText, byte[] hashText)
        {
            bool var0000;
            try
            {
                if (this.m_cryptProviderType != CryptographyProviderType.HashProvider)
                {
                    throw new InvalidOperationException();
                }
                if (plainText == null)
                {
                    throw new ArgumentNullException("plainText");
                }
                if (hashText == null)
                {
                    throw new ArgumentNullException("hashText");
                }
                var0000 = Cryptographer.CompareHash(this.m_cryptKey, plainText, hashText);
            }
            catch
            {
                throw;
            }
            return var0000;
        }

        public bool CompareHash(string plainText, string hashText)
        {
            bool var0000;
            try
            {
                if (this.m_cryptProviderType != CryptographyProviderType.HashProvider)
                {
                    throw new InvalidOperationException();
                }
                if (string.IsNullOrEmpty(plainText))
                {
                    throw new ArgumentNullException("plainText");
                }
                if (string.IsNullOrEmpty(hashText))
                {
                    throw new ArgumentNullException("hashText");
                }
                var0000 = Cryptographer.CompareHash(this.m_cryptKey, plainText, hashText);
            }
            catch
            {
                throw;
            }
            return var0000;
        }

        public byte[] CreateHash(byte[] plainText)
        {
            byte[] var0000;
            try
            {
                if (this.m_cryptProviderType != CryptographyProviderType.HashProvider)
                {
                    throw new InvalidOperationException();
                }
                if (plainText == null)
                {
                    throw new ArgumentNullException("plainText");
                }
                var0000 = Cryptographer.CreateHash(this.m_cryptKey, plainText);
            }
            catch
            {
                throw;
            }
            return var0000;
        }

        public string CreateHash(string plainText)
        {
            string var0000;
            try
            {
                if (this.m_cryptProviderType != CryptographyProviderType.HashProvider)
                {
                    throw new InvalidOperationException();
                }
                if (string.IsNullOrEmpty(plainText))
                {
                    throw new ArgumentNullException("plainText");
                }
                var0000 = Cryptographer.CreateHash(this.m_cryptKey, plainText);
            }
            catch
            {
                throw;
            }
            return var0000;
        }

        public static ProtectedKey CreateProtectedKey(byte[] plainText, DataProtectionScope protectionScope)
        {
            ProtectedKey var0000;
            try
            {
                var0000 = ProtectedKey.CreateFromPlaintextKey(plainText, protectionScope);
            }
            catch
            {
                throw;
            }
            return var0000;
        }

        public string Decrypt(string cypherText)
        {
            string var0000;
            try
            {
                if (this.m_cryptProviderType != CryptographyProviderType.SymmetricCryptoProvider)
                {
                    throw new InvalidOperationException();
                }
                if (string.IsNullOrEmpty(cypherText))
                {
                    throw new ArgumentNullException("cypherText");
                }
                var0000 = Cryptographer.DecryptSymmetric(this.m_cryptKey, cypherText);
            }
            catch(Exception ex)
            {
                throw;
            }
            return var0000;
        }

        public byte[] Decrypt(byte[] argCypherText)
        {
            byte[] var0000;
            try
            {
                if (this.m_cryptProviderType != CryptographyProviderType.SymmetricCryptoProvider)
                {
                    throw new InvalidOperationException();
                }
                if (argCypherText == null)
                {
                    throw new ArgumentNullException("argCypherText");
                }
                var0000 = Cryptographer.DecryptSymmetric(this.m_cryptKey, argCypherText);
            }
            catch
            {
                throw;
            }
            return var0000;
        }

        public byte[] Encrypt(byte[] plainText)
        {
            byte[] var0000;
            try
            {
                if (this.m_cryptProviderType != CryptographyProviderType.SymmetricCryptoProvider)
                {
                    throw new InvalidOperationException();
                }
                if (plainText == null)
                {
                    throw new ArgumentNullException("plainText");
                }
                var0000 = Cryptographer.EncryptSymmetric(this.m_cryptKey, plainText);
            }
            catch
            {
                throw;
            }
            return var0000;
        }

        public string Encrypt(string plainText)
        {
            string var0000;
            try
            {
                if (this.m_cryptProviderType != CryptographyProviderType.SymmetricCryptoProvider)
                {
                    throw new InvalidOperationException();
                }
                if (string.IsNullOrEmpty(plainText))
                {
                    throw new ArgumentNullException("plainText");
                }
                var0000 = Cryptographer.EncryptSymmetric(this.m_cryptKey, plainText);
            }
            catch
            {
                throw;
            }
            return var0000;
        }

        public static void WriteProtectedKey(Stream keyStream, ProtectedKey proctKey)
        {
            try
            {
                KeyManager.Write(keyStream, proctKey);
            }
            catch
            {
                throw;
            }
        }
    }
}

