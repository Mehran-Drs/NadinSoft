using FluentValidation;
using NadinSoft.Application.CQRS.Authentication.Command;

namespace NadinSoft.Application.Validations.Authentication
{
    public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidation()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name Can Not Be Empty")
                .MaximumLength(50)
                .WithMessage("Maximum Length Is 50 Character");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name Can Not Be Empty")
                .MaximumLength(50)
                .WithMessage("Maximum Length Is 50 Character");

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email Can Not Be Empty")
               .MaximumLength(250)
               .WithMessage("Maximum Length Is 250 Character");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password Can Not Be Empty");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password Can Not Be Empty")
                .Equal(x=>x.Password)
                .WithMessage("Password And Confirm Password Are Not Same");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone Number Can Not Be Empty")
                .MaximumLength(11)
                .WithMessage("Maximum Length Is 11 Character");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("User Name Can Not Be Empty")
                .MaximumLength(50)
                .WithMessage("Maximum Length Is 50Character");
        }
    }
}
