using System.Security.Cryptography;
using System.Text;
using IAM.Seedwork.Abstractions;

namespace IAM.Seedwork
{
    /// <inheritdoc/>
    internal class SHAHashCalculator : IHashCalculator
    {
        /// <inheritdoc/>
        string IHashCalculator.Compute(string value)
        {
            // Create a SHA256
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                // Convert byte array to a string
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
