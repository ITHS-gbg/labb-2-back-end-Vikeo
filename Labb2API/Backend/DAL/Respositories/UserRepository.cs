using Labb2API.Backend.DAL.Contexts;
using Labb2API.Backend.DAL.Models;
using Labb2API.Backend.DAL.Respositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2API.Backend.DAL.Respositories;

public class UserRepository : IUserRepository
{

    private readonly WebsiteContext _websiteContext;

    public UserRepository(WebsiteContext websiteContext)
    {
        _websiteContext = websiteContext;
    }

    public bool CreateUser(User user)
    {
        //TODO Always false
        if (user is null) return false;

        //Makes it so that two users can't have the same email.
        if (GetAllUsers().Exists(u => u.Email == user.Email))
        {
            return false;
        }
        _websiteContext.Users.Add(user);
        return true;
    }

    public List<User> GetAllUsers()
    {
        return _websiteContext.Users.Include(u => u.ActiveCourses).ToList();
    }

    //TODO Ska jag ha async lr not?
    public User? GetUser(string email)
    {
        //TODO ändra till en rad. Debug user.
        //TODO Hur får jag till activecourses på users?
        //TODO Include Course på denna också.
        var user = _websiteContext.Users.Include(u => u.ActiveCourses).FirstOrDefault(u => u.Email == email);
        return user;
    }

    //PUT Replace the properties of a user.
    //TODO Is it PUT if you don't replace the whole user?
    public bool UpdateUser(User user)
    {
        var existingUser = _websiteContext.Users.FirstOrDefault(u => u.Email == user.Email);

        if (existingUser is null)
        {
            return false;
        }

        //TODO Can't change email right now, don't know how I want to implement it.
        //existingUser.Email = user.Email;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Phone = user.Phone;
        existingUser.Address = user.Address;

        return true;
    }

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

        if (!_websiteContext.Users.ToList().Exists(u => u.Email == user.Email))
        {
            return false;
        }

        _websiteContext.Users.Remove(user);

        return true;
    }
}