using System.Security.Cryptography;
using System.Text;

namespace Cut_Sheet
{
    public static class HashHelper
    {
        /// <summary>
        /// Trả về chuỗi MD5 hex 32 ký tự của input.
        /// </summary>
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input ?? string.Empty));
                var sb = new StringBuilder(32);
                foreach (var b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
