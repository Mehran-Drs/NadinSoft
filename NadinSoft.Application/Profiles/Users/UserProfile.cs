using AutoMapper;
using NadinSoft.Application.CQRS.Authentication.Command;
using NadinSoft.Domain.Entities.Users;

namespace NadinSoft.Application.Profiles.Users
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserCommand, User>();
        }
    }
}
