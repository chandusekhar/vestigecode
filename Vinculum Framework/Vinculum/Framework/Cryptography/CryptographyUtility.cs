namespace Vinculum.Framework.Cryptography
{
    using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
    using System;

    public static class CryptographyUtility
    {
        public static byte[] CombineBytes(byte[] buffer1, byte[] buffer2)
        {
            return Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.CryptographyUtility.CombineBytes(buffer1, buffer2);
        }

        public static bool CompareBytes(byte[] byte1, byte[] byte2)
        {
            return Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.CryptographyUtility.CompareBytes(byte1, byte2);
        }

        public static byte[] GetBytesFromHexString(string hexadecimalNumber)
        {
            return Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.CryptographyUtility.GetBytesFromHexString(hexadecimalNumber);
        }

        public static string GetHexStringFromBytes(byte[] bytes)
        {
            return Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.CryptographyUtility.GetHexStringFromBytes(bytes);
        }

        public static void GetRandomBytes(byte[] bytes)
        {
            Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.CryptographyUtility.GetRandomBytes(bytes);
        }

        public static byte[] GetRandomBytes(int size)
        {
            return Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.CryptographyUtility.GetRandomBytes(size);
        }

        public static void ZeroOutBytes(byte[] bytes)
        {
            Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.CryptographyUtility.ZeroOutBytes(bytes);
        }
    }
}

