using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NadinSoft.Domain.Entities.Users;

namespace NadinSoft.Application.CQRS.Authentication.Command
{
    internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IValidator<RegisterUserCommand> _validator;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper, IValidator<RegisterUserCommand> validator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken); 

            var user = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return user.Id;
            }

            throw new ValidationException(result.Errors.Select(x => new FluentValidation.Results.ValidationFailure()
            {
                ErrorCode = x.Code,
                ErrorMessage = x.Description
            }));
        }
    }
}
