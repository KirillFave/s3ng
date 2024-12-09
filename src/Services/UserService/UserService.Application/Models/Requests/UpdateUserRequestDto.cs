using MediatR;
using UserService.Models.Response;

namespace UserService.Models.Requests
{
    public class UpdateUserRequestDto : IRequest<UpdateUserResponseDto>
    {
        public required Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
    }
}