using Labb2API.DAL;
using Labb2API.DAL.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Denna g�r att [FromServices] funkar, man l�gger till en instans av en UserStorage.
builder.Services.AddSingleton<UserStorage>();

var app = builder.Build();

app.MapGet("/", () => "Testing!");

//USER-relaterade metoder.

//GET, h�mta alla users.
app.MapGet("/users", ([FromServices] UserStorage storage) =>
{
    var users = storage.GetAllUsers();

    if (users.Count <= 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(users);
});

//GET, h�mta en user
app.MapGet("/users/{email}", ([FromServices] UserStorage storage, string email) =>
{
    var user = storage.GetUser(email);

    if (user is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
});

//POST, Skapa en user.
app.MapPost("/users", ([FromServices] UserStorage storage, User user) =>
{
    if (string.IsNullOrEmpty(user.Email))
    {
        return Results.BadRequest();
    }

    if (storage.CreateUser(user))
    {
        return Results.Ok();
    }
    return Results.Conflict();
});

//PATCH/PUT, Uppdatera en user.

//PATCH, uppdatera en users lista av aktiva kurser.

//DELETE, ta bort en user.

//COURSE-relaterade metoder.
//GET, h�mta alla courses.
//GET, h�mta en course.
//POST, skapa en course.
//PATCH/PUT, uppdatera en course.
//PATCH, uppdatera status p� en course
//DELETE, ta bort en course.

app.Run();