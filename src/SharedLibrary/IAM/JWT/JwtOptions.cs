namespace SharedLibrary.IAM.JWT
{
    public class JwtOptions
    {
        public const string Jwt = nameof(JwtOptions);
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expires { get; set; }
    }
}
