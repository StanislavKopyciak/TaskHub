using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHub.Application.Services.TaskService.Query.GetAllNotCompletedTask
{
    public class GetAllNotCompletedTaskValidator : AbstractValidator<GetAllNotCompletedTaskQuery>
    {
        public GetAllNotCompletedTaskValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.State)
                .Equal(Core.Enums.State.NotCompleted);
        }
    }
}
