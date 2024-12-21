namespace IAM.Seedwork
{
    public class JwtOptions
    {
        public const string Jwt = nameof(Jwt);
        public string SecretKey { get; set; }
        public int ExpireHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
