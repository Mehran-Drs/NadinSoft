using FluentValidation;
using NadinSoft.Application.CQRS.Authentication.Query;

namespace NadinSoft.Application.Validations.Authentication
{
    public class LoginQueryValidation : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName Can Not Be Empty");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password Can Not Be Empty");
        }
    }
}
