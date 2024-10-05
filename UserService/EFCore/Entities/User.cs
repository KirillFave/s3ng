namespace UserService.EFCore.Entities;

public class User : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public long Phone { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }
}