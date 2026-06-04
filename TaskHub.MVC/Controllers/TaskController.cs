using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Services.TaskService;
using TaskHub.Application.Services.TaskService.Command.CompleteTask;
using TaskHub.Application.Services.TaskService.Command.CreateTask;
using TaskHub.Application.Services.TaskService.Command.UpdateTask;
using TaskHub.Application.Services.TaskService.Query.GetAllByUserIdAndState;
using TaskHub.Application.Services.TaskService.Query.GetTask;
using TaskHub.MVC.Models;


namespace TaskHub.MVC.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IValidator<CreateTaskCommand> _validatorTaskCreate;
        private readonly IValidator<UpdateTaskCommand> _validatorTaskUpdate;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ProcessService _processService;

        public TaskController(
            IValidator<CreateTaskCommand> validatorCreate, 
            IValidator<UpdateTaskCommand> validatorUpdate,
            IMapper mapper,
            IMediator mediator,
            ProcessService processService)
        {
            _validatorTaskCreate = validatorCreate;
            _validatorTaskUpdate = validatorUpdate;
            _mapper = mapper;
            _mediator = mediator;
            _processService = processService;
        }

        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public IActionResult Create() => View();


        [HttpGet("/Task/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id not required");

            var userId = CurrentUserId;

            var command = new GetTaskQuery
            {
                TaskId = id,
                UserId = userId
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return RedirectToAction("Index");
            }

            var dto = result.Value; 
            return View(dto);
        }

        [HttpPatch("/Task/Edit")]
        public async Task<IActionResult> Edit(TaskItemDTO dto)
        {

            var userId = CurrentUserId;

            var command = _mapper.Map<UpdateTaskCommand>(dto);
            command.UserId = userId;

            var validationResult = await _validatorTaskUpdate.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Json(new
                {
                    success = false,
                    errors = validationResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage })
                });
            }

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return Json(new { success = false, errors = new[] { new { property = "", message = result.Error } } });
            }

            return Json(new { success = true, data = result.Value });
        }

        [HttpGet("/Task/CompletedList")]
        public async Task<IActionResult> CompletedList(CancellationToken ct)
        {
            var userId = CurrentUserId;

            await _processService.UpdateTaskStateAsync(userId, ct);

            var tasks = await _mediator.Send(new GetAllByUserIdAndStateQuery
            {
                UserId = userId,
                State = Core.Enums.State.Completed
            });

            return View(tasks);
        }

        [HttpGet("/Task/Index")]
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var userId = CurrentUserId;

            await _processService.UpdateTaskStateAsync(userId, ct);

            var tasks = await _mediator.Send(new GetAllByUserIdAndStateQuery
            {
                UserId = userId,
                State = Core.Enums.State.NotCompleted
            });

            return View(tasks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("/Task/Create")]
        public async Task<IActionResult> Create(TaskCreateDTO dto)
        {
            var userId = CurrentUserId;


            var command = _mapper.Map<CreateTaskCommand>(dto);
            command.UserId = userId;

            var validationResult = await _validatorTaskCreate.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Json(new
                {
                    success = false,
                    errors = validationResult.Errors.Select(e => new
                    {
                        property = e.PropertyName,
                        message = e.ErrorMessage
                    })
                });
            }

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return Json(new
                {
                    success = false,
                    errors = new[] { new {
                        property = "",
                        message = result.Error
                    }}
                });
            }

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index", "Task")
            });
        }

        [HttpGet("/Task/Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id not required");

            var userId = CurrentUserId;

            var result = await _mediator.Send(new GetTaskQuery
            {
                TaskId = id,
                UserId = userId
            });

            if (!result.Success)
                return NotFound(result.Error);

            return View(result.Value);
        }


        [HttpPost("/Task/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Error"] = "Id not required.";
                return RedirectToAction("Index");
            }

            var userId = CurrentUserId;

            var result = await _mediator.Send(new DeleteTaskCommand
            {
                Id = id,
                UserId = userId
            });

            if (!result.Success)
            {
                TempData["Error"] = result.Error;
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Task deleted";
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Complete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id not required.");

            var userIdString = CurrentUserId;
            var result = await _mediator.Send(new CompleteTaskCommand(id, userIdString));

            if (!result)
                return NotFound();

            return RedirectToAction("Index");
        }
    }
}




