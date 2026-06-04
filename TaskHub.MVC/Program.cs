using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskHub.Application.Common.Mappings;
using TaskHub.Application.Interfaces;
using TaskHub.Application.Services.TaskService;
using TaskHub.Application.Services.TaskService.Command.CompleteTask;
using TaskHub.Application.Services.TaskService.Command.CreateTask;
using TaskHub.Application.Services.TaskService.Command.UpdateTask;
using TaskHub.Application.Services.TaskService.Query.GetAllByUserIdAndState;
using TaskHub.Application.Services.TaskService.Query.GetAllTasks;
using TaskHub.Application.Services.TaskService.Query.GetTask;
using TaskHub.Application.Services.UserService.Auth.Command.SignIn;
using TaskHub.Application.Services.UserService.Auth.Command.SignUp;
using TaskHub.Core.Model;
using TaskHub.Infrastructure.Data;
using TaskHub.Infrastructure.Data.Repository;
using TaskHub.Infrastructure.Services.Auth;
using TaskHub.MVC.HttpCookieService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TaskHubContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddHttpClient<IEmailService, EmailService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});
builder.Services.AddScoped<IEmailVerificationRepository, EmailVerificationRepository>();

builder.Services.AddHttpClient();

builder.Services.AddTransient<CookieService>();
builder.Services.AddScoped<IValidator<SignUpCommand>, SignUpValidator>();
builder.Services.AddScoped<IValidator<SignInCommand>, SignInValidator>();
builder.Services.AddScoped<IValidator<CreateTaskCommand>, CreateTaskValidator>();
builder.Services.AddScoped<IValidator<UpdateTaskCommand>, UpdateTaskValidator>();
builder.Services.AddTransient<ProcessService>();



builder.Services.AddAutoMapper(typeof(UserProfile), typeof(TaskProfile));

builder.Services.Configure<PBKDF2Section>(builder.Configuration.GetSection("PBKDF2"));


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<SignInCommand>();
    cfg.RegisterServicesFromAssemblyContaining<SignUpCommand>();
    cfg.RegisterServicesFromAssemblyContaining<CreateTaskCommand>();
    cfg.RegisterServicesFromAssemblyContaining<GetTaskQuery>();
    cfg.RegisterServicesFromAssemblyContaining<DeleteTaskCommand>();
    cfg.RegisterServicesFromAssemblyContaining<UpdateTaskCommand>();
    cfg.RegisterServicesFromAssemblyContaining<GetAllTasksQuery>();
    cfg.RegisterServicesFromAssemblyContaining<CompleteTaskCommand>();
    cfg.RegisterServicesFromAssemblyContaining<GetAllByUserIdAndStateQuery>();
});

var jwtKey = builder.Configuration["JWT:Secret"] ?? throw new Exception("Secret key not found");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["jwt"];
                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.Redirect("/Auth/SignIn");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/Task/Index"));

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
