using MediatR;
using Microsoft.AspNetCore.Identity;

namespace NadinSoft.Application.CQRS.Authentication.Command
{
    public class RegisterUserCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
