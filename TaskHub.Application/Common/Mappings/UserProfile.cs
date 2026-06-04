using AutoMapper;
using TaskHub.Application.DTO.User;
using TaskHub.Application.Services.UserService.Auth.Command.SignIn;
using TaskHub.Application.Services.UserService.Auth.Command.SignUp;
using TaskHub.Application.Services.UserService.Auth.Command.VerifyEmail;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserSignInDTO, User>().ReverseMap();
            CreateMap<UserSignUpDTO, User>().ReverseMap();
            CreateMap<SignUpCommand, UserDTO>().ReverseMap();
            CreateMap<SignInCommand, UserDTO>().ReverseMap();
            CreateMap<UserSignInDTO, SignInCommand>().ReverseMap();
            CreateMap<UserSignUpDTO, SignUpCommand>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<VerifyEmailDTO, VerifyEmailCommand>().ReverseMap();
        }
    }
}
