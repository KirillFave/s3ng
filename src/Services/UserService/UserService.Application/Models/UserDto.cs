namespace UserService.Application.Models;

public class UserDto
{
    public required string Id { get; set; }
    public required string AuthenticationId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long PhoneNumber { get; set; }
    public required string Address { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required RoleDto Role { get; set; }
}