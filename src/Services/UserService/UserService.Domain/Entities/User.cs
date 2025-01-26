namespace UserService.Domain.Entities;

public class User : IEntity<Guid>
{
    public required Guid Id { get; set; }
    public required Guid AuthenticationId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public RoleEntity Role { get; set; }
    public long Phone { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; }
}
