using Labb2API.Backend.DAL.Models;

namespace Labb2API.Backend.DAL.Respositories.Interfaces;

public interface IUserRepository
{
    //TODO Maybe make async
    bool CreateUser(User user);
    List<User> GetAllUsers();
    User? GetUser(string email);
    bool UpdateUser(User user);
    bool AddActiveCourse(User user, Course course);
    bool RemoveActiveCourse(User user, Course course);
    bool RemoveUser(User user);
}