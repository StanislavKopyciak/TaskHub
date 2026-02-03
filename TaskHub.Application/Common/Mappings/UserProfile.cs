using AutoMapper;
using TaskHub.Application.DTO.User;
using TaskHub.Application.Services.UserService.Auth.Command.SignIn;
using TaskHub.Application.Services.UserService.Auth.Command.SignUp;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserSignInDTO, User>();
            CreateMap<UserSignUpDTO, User>();
            CreateMap<SignUpCommand, UserDTO>();
            CreateMap<SignInCommand, UserDTO>();
            CreateMap<UserSignInDTO, SignInCommand>();
            CreateMap<UserSignUpDTO, SignUpCommand>();
            CreateMap<User, UserDTO>();
        }
    }
}
