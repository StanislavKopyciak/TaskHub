using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Application.Services.UserService.Auth.Command.SignUp;
using TaskHub.Application.DTO.User;
using MediatR;
using TaskHub.Application.Services.UserService.Auth.Command.SignIn;
using TaskHub.Infrastructure.HttpCookieService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace TaskHub.MVC.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly CookieService _cookieService;
        private readonly IValidator<SignUpCommand> _signUpValidator;
        private readonly IValidator<SignInCommand> _signInValidator;

        public AuthController(
            IMapper mapper,
            IMediator mediator,
            CookieService cookieService,
            IValidator<SignUpCommand> signUpValidator,
            IValidator<SignInCommand> signInValidator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _cookieService = cookieService;
            _signUpValidator = signUpValidator;
            _signInValidator = signInValidator;
        }
        [HttpGet]
        public IActionResult SignIn() => View();
        [HttpGet]
        public IActionResult SignUp() => View();

        [HttpPost("/Auth/SignUp")]
        public async Task<IActionResult> SignUp(UserSignUpDTO dto)
        {
            var command = _mapper.Map<SignUpCommand>(dto);

            var validationResult = await _signUpValidator.ValidateAsync(command);

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

            await _cookieService.SetCookieAsync(result.Value!);

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index", "Task")
            });
        }

        [HttpPost("/Auth/SignIn")]
        public async Task<IActionResult> SignIn(UserSignInDTO dto)
        {
            var command = _mapper.Map<SignInCommand>(dto);

            var validationResult = await _signInValidator.ValidateAsync(command);

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

            await _cookieService.SetCookieAsync(result.Value!);

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index", "Task")
            });
        }

        [HttpPost("/Auth/SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await _cookieService.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
    }
}
