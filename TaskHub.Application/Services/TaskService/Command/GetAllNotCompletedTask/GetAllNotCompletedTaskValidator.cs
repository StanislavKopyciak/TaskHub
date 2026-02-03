using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub.Application.Services.TaskService.Command.GetAllNotCompletedTask
{
    public class GetAllNotCompletedTaskValidator : AbstractValidator<GetAllNotCompletedTaskCommand>
    {
        public GetAllNotCompletedTaskValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.State)
                .Equal(Core.Enums.State.NotCompleted);
        }
    }
}
