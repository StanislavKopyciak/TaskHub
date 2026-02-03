using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using TaskHub.Application.Services.TaskService;
using TaskHub.Core.Entities;
using TaskHub.Core.Interfaces;
using TaskHub.Infrastructure.Data;
using TaskHub.Infrastructure.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// ?? 1. Додаємо DbContext
// ===============================
builder.Services.AddDbContext<TaskHubContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ===============================
// ?? 2. Додаємо репозиторії
// ===============================
builder.Services.AddScoped<ITaskRepository<TaskItem>, TaskRepository>();

// ===============================
// ?? 3. Додаємо TaskService
// ===============================
builder.Services.AddScoped<ITaskService, TaskService>();

// ===============================
// ?? 4. AutoMapper
// ===============================
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ===============================
// ?? 5. Controllers
// ===============================
builder.Services.AddControllers();

// ===============================
// ?? 6. Swagger
// ===============================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===============================
// ?? 7. Middleware
// ===============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
