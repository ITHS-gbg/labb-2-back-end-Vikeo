using Labb2API.DAL;
using Labb2API.DAL.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Denna gör att [FromServices] funkar, man lägger till en instans av en UserStorage.
builder.Services.AddSingleton<UserStorage>();
builder.Services.AddSingleton<CourseStorage>();


var app = builder.Build();

app.MapGet("/", () => "Testing!");

#region User-relaterade metoder
//GET, hämta alla users.
app.MapGet("/users", ([FromServices] UserStorage userStorage) =>
{
    var users = userStorage.GetAllUsers();

    if (users.Count <= 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(users);
});

//GET, hämta en user
app.MapGet("/users/{email}", ([FromServices] UserStorage userStorage, string email) =>
{
    var user = userStorage.GetUser(email);

    if (user is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
});

//POST, Skapa en user.
app.MapPost("/users", ([FromServices] UserStorage userStorage, User user) =>
{
    if (string.IsNullOrEmpty(user.Email))
    {
        return Results.BadRequest();
    }

    if (userStorage.CreateUser(user))
    {
        return Results.Ok();
    }
    return Results.Conflict();
});

//PUT, Uppdatera en user.
app.MapPut("/users/{email}",
    ([FromServices] UserStorage userStorage, string email, User user
) =>
    {
        if (user.Email != email)
        {
            return Results.NotFound("email kan du inte ändra på");
        }

        userStorage.UpdateUser(user);


        return Results.Ok();
    });

//POST, Lägg till en course i activecourses.
app.MapPost("/users/{email}/{courseId}",
    ([FromServices] UserStorage userStorage, CourseStorage courseStorage, string email, int courseId) =>
{
    var course = courseStorage.GetCourse(courseId);
    var user = userStorage.GetUser(email);

    if (course is null || user is null)
    {
        return Results.NotFound();
    }

    return userStorage.AddActiveCourse(user, course) ? Results.Ok() : Results.Conflict();

});

//DELETE, ta bort en course från activecourses.
app.MapDelete("/users/{email}/{courseId}",
    ([FromServices] UserStorage userStorage, CourseStorage courseStorage, string email, int courseId) =>
    {
        var course = courseStorage.GetCourse(courseId);
        var user = userStorage.GetUser(email);

        if (course is null || user is null)
        {
            return Results.BadRequest();
        }

        return userStorage.RemoveActiveCourse(user, course) ? Results.Ok() : Results.NotFound();
    });

//DELETE, ta bort en user.
app.MapDelete("/users/{email}", ([FromServices] UserStorage userStorage, string email) =>
{
    var user = userStorage.GetUser(email);

    if (user is null)
    {
        return Results.NotFound(user);
    }

    userStorage.DeleteUser(user);
    return Results.Ok();

});
#endregion

#region Course-relaterade metoder
//GET, hämta alla courses.
app.MapGet("/courses", ([FromServices] CourseStorage courseStorage) =>
{
    var courses = courseStorage.GetAllCourses();

    if (courses.Count <= 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(courses);
});

//GET, hämta en course.
app.MapGet("/courses/{courseId}", ([FromServices] CourseStorage courseStorage, int courseId) =>
{
    var course = courseStorage.GetCourse(courseId);

    if (course is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(course);
});

//POST, skapa en course.
app.MapPost("/courses", ([FromServices] CourseStorage courseStorage, Course course) =>
{
    //TODO Kan jag verkligen kolla ID:t här?
    if (course.Id < 0)
    {
        return Results.BadRequest();
    }

    if (courseStorage.CreateCourse(course))
    {
        return Results.Ok();
    }

    return Results.Conflict();

});

//PUT, uppdatera en course.
app.MapPut("/courses/{courseId}", ([FromServices] CourseStorage courseStorage, Course course, int courseId) =>
{
    //TODO Kan jag verkligen kolla ID här, spelar det ens någon roll?
    if (course.Id < 0)
    {
        return Results.BadRequest();
    }

    if (courseStorage.UpdateCourse(courseId, course))
    {
        return Results.Ok();
    }

    return Results.Conflict("Det finns redan an kurs med samma ID som det nya.");
});

//PATCH, uppdatera status på en course
app.MapMethods("/courses/status/{courseId}", new[] {"PATCH"}, ([FromServices] CourseStorage courseStorage, int courseId, int status) =>
{
    var course = courseStorage.GetCourse(courseId);
    if (course is null)
    {
        return Results.NotFound();
    }

    return courseStorage.UpdateCourseStatus(course, status) ? Results.Ok() : Results.BadRequest();
});

//PATCH, uppdatera difficulty på en course
app.MapMethods("/courses/difficulty/{courseId}", new[] { "PATCH" }, ([FromServices] CourseStorage courseStorage, int courseId, int difficulty) =>
{
    var course = courseStorage.GetCourse(courseId);
    if (course is null)
    {
        return Results.NotFound();
    }

    return courseStorage.UpdateCourseDifficulty(course, difficulty) ? Results.Ok() : Results.BadRequest();
});
//DELETE, ta bort en course. 
app.MapDelete("/courses/{courseId}", ([FromServices] CourseStorage courseStorage, int courseId) =>
{

    return courseStorage.DeleteCourse(courseId) ? Results.Ok() : Results.NotFound();

});
#endregion

app.Run();