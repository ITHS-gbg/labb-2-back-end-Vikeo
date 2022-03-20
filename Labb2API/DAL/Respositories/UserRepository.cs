using Labb2API.DAL.Contexts;
using Labb2API.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Labb2API.DAL.Respositories;

public class UserRepository : IUserRepository, IDisposable
{

    private readonly WebsiteContext _websiteContext;
    private readonly IDictionary<string, User> _users;

    //private int _id;

    public UserRepository(WebsiteContext websiteContext)
    {
        _users = new Dictionary<string, User>();
        _websiteContext = websiteContext;
    }

    public bool CreateUser(User user)
    {
        //TODO Alltid falskt
        if (user is null) return false;

        //TODO Använder en sån här variablen på massa andra ställen, kanske bör göra det här också?
        //var existingUser = _websiteContext.Users.FirstOrDefault(u => u.Email == user.Email);
        if (GetAllUsers().Exists(u => u.Email == user.Email))
        {
            return false;
        }
        _websiteContext.Users.Add(user);
        return true;
    }

    public List<User> GetAllUsers()
    {
        return _websiteContext.Users.ToList();
    }

    //Kollar om det finns en user med en email, returnerar om den finns.
    public User? GetUser(string email)
    {
        //TODO ändra till en rad. Debug user.
        var user = _websiteContext.Users.FirstOrDefault(u => u.Email == email);
        return user;
    }

    //PUT Ändrar hela användaren.
    public bool UpdateUser(User user)
    {
        var existingUser = _websiteContext.Users.FirstOrDefault(u => u.Email == user.Email);

        if (existingUser is null)
        {
            return false;
        }

        //existingUser.Email = user.Email;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Phone = user.Phone;
        existingUser.Address = user.Address;



        return true;
    }

    //TODO Gör Add/Delete metoder istället för denna.
    //public bool UpdateActiveCourses(string email, List<Course> courses)
    //{
    //    if (courses.Count <= 0) return false;

    //    _users[email].ActiveCourses = courses;

    //    return true;
    //}

    public bool AddActiveCourse(User user, Course course)
    {
        var existingUser = _websiteContext.Users.FirstOrDefault(u => u.Email == user.Email);
        var matchedCourse = _websiteContext.Courses.FirstOrDefault(c => c.Id == course.Id);

        if (existingUser.ActiveCourses.Contains(matchedCourse))
        {
            return false;
        }

        if (matchedCourse is null)
        {
            return false;
        }

        existingUser.ActiveCourses.Add(matchedCourse);
        return true;
    }

    public bool RemoveActiveCourse(User user, Course course)
    {
        var existingUser = _websiteContext.Users.FirstOrDefault(u => u.Email == user.Email);
        var matchedCourse = _websiteContext.Courses.FirstOrDefault(c => c.Id == course.Id);

        if (matchedCourse is null)
        {
            return false;
        }

        if (existingUser is null)
        {
            return false;
        }

        existingUser.ActiveCourses.Remove(matchedCourse);
        return true;
    }

    public bool RemoveUser(User user)
    {
        //TODO Använder en sån här variablen på massa andra ställen, kanske bör göra det här också?
        //var existingUser = _websiteContext.Users.FirstOrDefault(u => u.Email == user.Email);
        if (!_websiteContext.Users.ToList().Exists(u => u.Email == user.Email))
        {
            return false;
        }

        //if (!_users.Keys.Contains(user.Email)) return false;

        _websiteContext.Users.Remove(user);

        return true;
    }

    public void Dispose()
    {
        //TODO Ska man ens ha dispose?
        _websiteContext.Dispose();
        GC.SuppressFinalize(this);
    }
}