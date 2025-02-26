using StackExchange.Redis;

namespace IAM.Repositories
{
    public class RefreshTokenRepository(IConnectionMultiplexer redis)
    {
        private readonly IDatabase _redisDb = redis.GetDatabase();

        private const string KeyPattern = "refresh_token:";

        private string GenerateTokenKey(string refreshToken) => $"{KeyPattern}{refreshToken}";

        public async Task SaveRefreshToken(string refreshToken, Guid userId)
        {
            var key = GenerateTokenKey(refreshToken);
            await _redisDb.StringSetAsync(key, userId.ToString(), TimeSpan.FromDays(7));
        }

        public async Task<Guid?> ValidateRefreshToken(string refreshToken)
        {
            var key = $"refresh_token:{refreshToken}";
            var userId = await _redisDb.StringGetAsync(key);

            if (!userId.HasValue)
                return null;

            return Guid.Parse(userId);
        }

        public async Task RemoveRefreshToken(string refreshToken)
        {
            var key = GenerateTokenKey(refreshToken);
            await _redisDb.KeyDeleteAsync(key);
        }
    }
}
