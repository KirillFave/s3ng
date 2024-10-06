namespace UserService.EFCore.Entities;

public class User : IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Login { get; set; }
    public required string PasswordHash { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Role Role { get; set; }
    public long Phone { get; set; }
    public required string Address { get; set; }
    public DateTime CreatedAt { get; set; }
}