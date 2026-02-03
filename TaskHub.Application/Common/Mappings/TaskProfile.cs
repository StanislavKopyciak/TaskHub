using AutoMapper;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.DTO.User;
using TaskHub.Application.Services.TaskService.Command.CreateTask;
using TaskHub.Application.Services.TaskService.Command.GetAllCompletedTask;
using TaskHub.Application.Services.TaskService.Command.UpdateTask;
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
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<TaskItemDTO, UpdateTaskCommand>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<TaskCreateDTO, TaskItem>();
            CreateMap<TaskItemDTO, TaskItem>();

            CreateMap<TaskItem, TaskCreateDTO>();
            CreateMap<TaskItem, TaskItemDTO>();

            CreateMap<CreateTaskCommand, TaskItem>();
            CreateMap<UpdateTaskCommand, TaskItem>();

            CreateMap<GetAllCompletedTaskCommand, TaskItem>();
           
        }
    }
}


