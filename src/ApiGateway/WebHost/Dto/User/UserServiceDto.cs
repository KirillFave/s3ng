namespace WebHost.Dto.User;

public class CreateUserRequestDto
{
    public required string AuthenticationId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long Phone { get; set; }
    public required string Address { get; set; }
    public required RoleDto Role { get; set; }
}

public enum UserResultDto
{
    Unspecified,
    Fail,
    Success,
    NotFound
}

public enum RoleDto
{
    Unspecified,
    Buyer,
    Seller,
    Moderator
}

public class UserDto
{
    public required string Id { get; set; }
    public required string AuthenticationId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long Phone { get; set; }
    public required string Address { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required RoleDto Role { get; set; }
}

public class UpdateUserRequestDto
{
    public required string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public long Phone { get; set; }
}
