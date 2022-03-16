using Labb2API.DAL;
using Labb2API.DAL.Models;
using Labb2API.DAL.Storages;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
//Denna gör att [FromServices] funkar, man lägger till en instans av en UserStorage.
builder.Services.AddSingleton<UserStorage>();
builder.Services.AddSingleton<CourseStorage>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGet("/", () => "Testing!");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();