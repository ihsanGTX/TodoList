using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Application.Services;
using TodoApp.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("https://localhost:50470")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//builder.Services.AddDbContext<TodoDbContext>(options =>
//    options.UseSqlite(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        b => b.MigrationsAssembly("TodoApp.Infrastructure")
//    ));

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite(
        "Data Source=C:\\Users\\walke\\source\\repos\\TodoList\\TodoList.Server\\TodoApp.API\\todo.db",
        b => b.MigrationsAssembly("TodoApp.Infrastructure")
    ));


builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<TodoService>();

var app = builder.Build();

app.UseCors("AllowReactApp");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
    c.RoutePrefix = "";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
