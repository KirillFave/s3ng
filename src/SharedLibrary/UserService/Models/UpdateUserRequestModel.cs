namespace SharedLibrary.UserService.Models
{
    public class UpdateUserRequestModel
    {
        public required Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
    }
}
