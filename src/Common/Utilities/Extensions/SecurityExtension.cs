using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public static class SecurityExtension
    {
        public static string GetHash(this string pass, string salt = null)
        {
            if(salt != null)
                pass += salt;
            else
                pass += "*7^U!c6&%";
            byte[] data = Encoding.ASCII.GetBytes(pass);

            #region هش با استفاده از MD5
            //using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            //{
            //    data = md5.ComputeHash(data);
            //}
            #endregion


            #region هش با استفاده از SHA512 - روش قدیمی
            //using (SHA512 shaM = new SHA512Managed())
            //{
            //    data = shaM.ComputeHash(data);
            //    string encPass = Encoding.ASCII.GetString(data);
            //    return encPass;
            //}
            #endregion


            #region هش با استفاده از SHA512 - روش جدید
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(data);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashedInputBytes)
                    sb.Append(b.ToString("X2"));

                var encPass = sb.ToString();
                return encPass;
            }
            #endregion

        }

    }
}