namespace SharedLibrary.UserService.Models
{
    public class CreateUserRequestModel
    {
        public required string AuthenticationId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public long Phone { get; set; }
        public required string Address { get; set; }
        public required RoleModel Role { get; set; }
    }
}
