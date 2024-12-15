using IAM.Entities;
using IAM.Seedwork.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IAM.Seedwork
{
    /// <inheritdoc/>
    internal sealed class JwtTokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;
        private const string _secretKeyConfigName = "JwtSecretKey";
        private const string _expiredHoursConfigName = "JwtTokenExpireHours";

        public JwtTokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc/>
        string ITokenProvider.GenerateToken(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var secretKey = _configuration[_secretKeyConfigName] ?? throw new ArgumentNullException(_secretKeyConfigName);
            var expiredHours = int.Parse(_configuration[_expiredHoursConfigName] ?? throw new ArgumentNullException(_expiredHoursConfigName));

            var claims = new Claim[] { new("userId", user.Id.ToString()) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(signingCredentials: signingCredentials, 
                claims: claims, expires: DateTime.Now.AddHours(expiredHours));  

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue.ToString();
        }
    }
}
