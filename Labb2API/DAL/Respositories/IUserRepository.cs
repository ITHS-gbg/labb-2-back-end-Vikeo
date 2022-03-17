using Labb2API.DAL.Models;

namespace Labb2API.DAL.Respositories;

public interface IUserRepository : IDisposable
{
    bool CreateUser(User user);
    List<User> GetAllUsers();
    User? GetUser(string email);
    bool UpdateUser(User user);
    bool AddActiveCourse(User user, Course course);
    bool RemoveActiveCourse(User user, Course course);
    bool RemoveUser(User user);
}