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
using TaskHub.Application.Services.TaskService.Query.GetAllCompletedTask;
using TaskHub.Application.Services.TaskService.Query.GetAllNotCompletedTask;
using TaskHub.Application.Services.TaskService.Query.GetTask;
using TaskHub.MVC.Models;


namespace TaskHub.MVC.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;
        private readonly IValidator<CreateTaskCommand> _validatorTaskCreate;
        private readonly IValidator<UpdateTaskCommand> _validatorTaskUpdate;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ProcessService _processService;

        public TaskController(
            ILogger<TaskController> logger, 
            ITaskService taskService, 
            IValidator<CreateTaskCommand> validatorCreate, 
            IValidator<UpdateTaskCommand> validatorUpdate,
            IMapper mapper,
            IMediator mediator,
            ProcessService processService)
        {
            _logger = logger;
            _taskService = taskService;
            _validatorTaskCreate = validatorCreate;
            _validatorTaskUpdate = validatorUpdate;
            _mapper = mapper;
            _mediator = mediator;
            _processService = processService;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpGet("/Task/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

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

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();

            if (userId is null)
                return Unauthorized();

            await _processService.UpdateTaskStateAsync(userId.Value);

            var tasks = await _mediator.Send(new GetAllCompletedTaskQuery
            {
                UserId = userId.Value
            });

            return View(tasks);
        }

        public async Task<IActionResult> CompletedList()
        {
            var userId = GetCurrentUserId();

            if (userId is null)
                return Unauthorized();

            await _processService.UpdateTaskStateAsync(userId.Value);

            var tasks = await _mediator.Send(new GetAllNotCompletedTaskQuery
            {
                UserId = userId.Value
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
            try
            {
                if (dto is null)
                {
                    return Json(new
                    {
                        success = false,
                        errors = new[]
                        {
                            new { property = "", message = "Дані завдання не були передані." }
                        }
                    });
                }

                var userId = GetCurrentUserId();

                if (userId is null)
                    return Unauthorized();

                var command = _mapper.Map<CreateTaskCommand>(dto);
                command.UserId = userId.Value;

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка створення завдання");
                return Json(new
                {
                    success = false,
                    errors = new[] { new { property = "", message = ex.Message } }
                });
            }
        }

        [HttpGet("/Task/Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Некоректний id завдання.");

            var userId = GetCurrentUserId();

            if (userId is null)
                return Unauthorized();

            var result = await _mediator.Send(new GetTaskQuery
            {
                TaskId = id,
                UserId = userId.Value
            });

            if (!result.Success)
                return NotFound(result.Error);

            return View(result.Value);
        }

        [HttpPatch("/Task/Edit/")]
        public async Task<IActionResult> Edit(TaskItemDTO dto)
        {
            if (dto is null)
            {
                return Json(new
                {
                    success = false,
                    errors = new[]
                    {
                        new { property = "", message = "Дані завдання не були передані." }
                    }
                });
            }
            var userId = GetCurrentUserId();

            if (userId is null)
                return Unauthorized();

            var command = _mapper.Map<UpdateTaskCommand>(dto);
            command.UserId = userId.Value;

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

        [HttpPost("/Task/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["Error"] = "Некоректний id завдання.";
                return RedirectToAction("Index");
            }

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var result = await _mediator.Send(new DeleteTaskCommand
            {
                Id = id,
                UserId = Guid.Parse(userIdString)
            });

            if (!result.Success)
            {
                TempData["Error"] = result.Error;
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Завдання видалено";
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Complete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Некоректний id завдання.");

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var result = await _mediator.Send(new CompleteTaskCommand(id, userIdString));

            if (!result)
                return NotFound();

            return RedirectToAction("Index");
        }


        private Guid? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return null;

            if (!Guid.TryParse(userIdClaim, out var userId))
                return null;

            return userId;
        }
    }
}




