using s3ng.IAM.Seedwork.Abstractions;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace s3ng.IAM.Seedwork
{
    internal class SHAHashCalculator : IHashCalculator
    {
        string IHashCalculator.Compute(string value)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}