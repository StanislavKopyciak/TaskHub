using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TaskHub.Application.Common.Mappings;
using TaskHub.Application.Services.TaskService;
using TaskHub.Application.Services.TaskService.Command.CreateTask;
using TaskHub.Application.Services.TaskService.Command.UpdateTask;
using TaskHub.Application.Services.UserService;
using TaskHub.Application.Services.UserService.Auth;
using TaskHub.Application.Services.UserService.Auth.Command.SignIn;
using TaskHub.Application.Services.UserService.Auth.Command.SignUp;
using TaskHub.Core.Entities;
using TaskHub.Core.Interfaces;
using TaskHub.Core.Model;
using TaskHub.Infrastructure.Data;
using TaskHub.Infrastructure.Data.Repository;
using TaskHub.Application.Services.TaskService.Commands.GetTask;
using TaskHub.Infrastructure.HttpCookieService;
using TaskHub.Application.Services.TaskService.Command.GetAllCompletedTask;
using TaskHub.Application.Services.TaskService.Command.GetAllNotCompletedTask;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TaskHubContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITaskRepository<TaskItem>, TaskRepository>();
builder.Services.AddTransient<IUserRepository<User>, UserRepository>();
builder.Services.AddTransient<CookieService>();
builder.Services.AddScoped<IValidator<SignUpCommand>,SignUpValidator>();
builder.Services.AddScoped<IValidator<SignInCommand>, SignInValidator>();
builder.Services.AddScoped<IValidator<CreateTaskCommand>, CreateTaskValidator>();
builder.Services.AddScoped<IValidator<UpdateTaskCommand>, UpdateTaskValidator>();
builder.Services.AddTransient<ProcessService>();


builder.Services.AddAutoMapper(typeof(UserProfile), typeof(TaskProfile));
builder.Services.Configure<TaskHub.Core.Model.PBKDF2Section>(builder.Configuration.GetSection("PBKDF2"));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<SignInHandler>();
    cfg.RegisterServicesFromAssemblyContaining<SignUpHandler>();
    cfg.RegisterServicesFromAssemblyContaining<CreateTaskHandler>();
    cfg.RegisterServicesFromAssemblyContaining<GetTaskHandler>();
    cfg.RegisterServicesFromAssemblyContaining<DeleteTaskHandler>();
    cfg.RegisterServicesFromAssemblyContaining<UpdateTaskHandler>();
    cfg.RegisterServicesFromAssemblyContaining<GetAllCompletedTaskCommand>();
    cfg.RegisterServicesFromAssemblyContaining<GetAllNotCompletedTaskCommand>();
});


builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "UserSignInCookie";
        config.LoginPath = "/Auth/SignIn";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Task/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();
