using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IAM.Entities;
using IAM.Seedwork.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IAM.Seedwork
{
    /// <inheritdoc/>
    internal sealed class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtOptions _options;

        public JwtTokenProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        /// <inheritdoc/>
        string ITokenProvider.GenerateToken(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var claims = new Claim[] { new("userId", user.Id.ToString()) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                signingCredentials: signingCredentials, 
                claims: claims, 
                audience: _options.Audience,
                issuer: _options.Issuer,
                expires: DateTime.Now.AddHours(_options.ExpireHours));  

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue.ToString();
        }
    }
}
