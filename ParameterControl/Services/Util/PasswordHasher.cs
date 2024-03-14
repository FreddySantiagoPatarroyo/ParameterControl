using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ParameterControl.Models;

namespace ParameterControl.Services.Util
{
    public class PasswordHasher
    {
        public static string HashPass(string textPass)
        {
            byte[] salt;
            byte[] buffer;

            if (textPass == null)
            {
                throw new ArgumentNullException(nameof(textPass));
            }

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(textPass, 16, 1000))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(32);
            }

            byte[] dst = new byte[49];
            Buffer.BlockCopy(salt,0,dst,1,16);
            Buffer.BlockCopy(buffer, 0, dst, 17, 32);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPass(string hashedPasswordStr, string textPass)
        {
            byte[] buffer4;
            if (hashedPasswordStr == null)
            {
                return false;
            }
            if (textPass == null)
            {
                throw new ArgumentException(nameof(textPass));
            }

            try
            {
                byte[] src = Convert.FromBase64String(hashedPasswordStr);
                if ((src.Length != 49) || (src[0] != 0))
                {
                    return false;
                }
                byte[] dst = new byte[16];
                Buffer.BlockCopy(src, 1, dst, 0, 16);
                byte[] buffer3 = new byte[32];
                Buffer.BlockCopy(src, 17, buffer3, 0, 32);

                using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(textPass, dst,1000))
                {
                    buffer4 = bytes.GetBytes(32);
                }
                return buffer3.SequenceEqual(buffer4);
            }
            catch (Exception ex)
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }
    }
}
