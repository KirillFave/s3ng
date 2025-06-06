namespace SharedLibrary.UserService.Models
{
    public class CreateUserRequestModel
    {
        public required Guid AuthenticationId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required long Phone { get; set; }
        public required string Address { get; set; }
        public required RoleModel Role { get; set; }
    }
}
