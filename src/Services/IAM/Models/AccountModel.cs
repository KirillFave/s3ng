namespace IAM.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
