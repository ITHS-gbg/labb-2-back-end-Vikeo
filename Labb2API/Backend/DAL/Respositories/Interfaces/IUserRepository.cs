using Labb2API.Backend.DAL.Models;

namespace Labb2API.Backend.DAL.Respositories.Interfaces;

public interface IUserRepository
{
    //TODO Maybe make async
    Task<bool> CreateUserAsync(User user);
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserAsync(string email);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> AddActiveCourseAsync(User user, Course course);
    Task<bool> RemoveActiveCourseAsync(User user, Course course);
    Task<bool> RemoveUserAsync(User user);
}