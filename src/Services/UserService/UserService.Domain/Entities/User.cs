namespace UserService.Domain.Entities;

public class User : IEntity<Guid>
{
    public required Guid Id { get; set; }
    public required Guid AuthenticationId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required RoleEntity Role { get; set; }
    public long Phone { get; set; }
    public required string Address { get; set; }
    public required DateTime CreatedAt { get; set; }
}