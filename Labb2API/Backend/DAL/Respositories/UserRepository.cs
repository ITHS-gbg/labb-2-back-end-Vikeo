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

    public async Task<bool> CreateUserAsync(User user)
    {
        //TODO Always false
        if (user is null) return false;

        //Makes it so that two users can't have the same email.
        //TODO double check to see if this works as intended. ^
        if ((await GetAllUsersAsync()).Exists(u => u.Email == user.Email))
        {
            return false;
        }
        await _websiteContext.Users.AddAsync(user);
        return true;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _websiteContext.Users.Include(u => u.ActiveCourses).ToListAsync();
    }

    //TODO Ska jag ha async lr not?
    public async Task<User?> GetUserAsync(string email)
    {
        //TODO ändra till en rad. Debug user.
        var user = await _websiteContext.Users.Include(u => u.ActiveCourses).FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    //PUT Replace the properties of a user.
    //TODO Is it PUT if you don't replace the whole user?
    public async Task<bool> UpdateUserAsync(User user)
    {
        var existingUser = await _websiteContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

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

    public async Task<bool> AddActiveCourseAsync(User user, Course course)
    {
        var existingUser = await _websiteContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        var matchedCourse = await _websiteContext.Courses.FirstOrDefaultAsync(c => c.Id == course.Id);

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

    public async Task<bool> RemoveActiveCourseAsync(User user, Course course)
    {
        var existingUser = await _websiteContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        var matchedCourse = await _websiteContext.Courses.FirstOrDefaultAsync(c => c.Id == course.Id);

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

    public async Task<bool> RemoveUserAsync(User user)
    {
        //TODO Double check if it does the right thing.
        if (!(await GetAllUsersAsync()).Exists(u => u.Email == user.Email))
        {
            return false;
        }

        _websiteContext.Users.Remove(user);

        return true;
    }
}