using AutoMapper;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.DTO.User;
using TaskHub.Application.Services.TaskService.Command.CreateTask;
using TaskHub.Application.Services.TaskService.Command.UpdateTask;
using TaskHub.Application.Services.TaskService.Query.GetAllByUserIdAndState;
using TaskHub.Application.Services.TaskService.Query.GetAllCompletedTask;
using TaskHub.Application.Services.UserService.Auth.Command.SignIn;
using TaskHub.Application.Services.UserService.Auth.Command.SignUp;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Common.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskCreateDTO, CreateTaskCommand>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()).ReverseMap();
            CreateMap<TaskItemDTO, UpdateTaskCommand>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()).ReverseMap();

            CreateMap<TaskCreateDTO, TaskItem>().ReverseMap();
            CreateMap<TaskItemDTO, TaskItem>().ReverseMap();

            CreateMap<TaskItem, TaskCreateDTO>().ReverseMap();
            CreateMap<TaskItem, TaskItemDTO>().ReverseMap();

            CreateMap<CreateTaskCommand, TaskItem>().ReverseMap();
            CreateMap<UpdateTaskCommand, TaskItem>().ReverseMap();

            CreateMap<GetAllByUserIdAndStateQuery, TaskItem>().ReverseMap();
           
        }
    }
}


