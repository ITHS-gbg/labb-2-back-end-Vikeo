using Labb2API.DAL;
using Labb2API.DAL.Contexts;
using Labb2API.DAL.Models;
using Labb2API.DAL.Respositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Detta ersätter OnConfiguring
builder.Services.AddDbContext<WebsiteContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("UserDbConnectionString");
    options.UseSqlServer(connectionString);
});

//TODO är detta rätt?
builder.Services.AddScoped<UnitOfWork>();

//Denna gör att [FromServices] funkar, man lägger till en instans av en UserStorage.

//TODO Tar vi bort AddSingleton??
//builder.Services.AddSingleton<UserRepository>();    
//builder.Services.AddSingleton<CourseRepository>();  

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGet("/", () => "Testing!");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();