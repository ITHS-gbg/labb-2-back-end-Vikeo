﻿using Labb2API.DAL.Models;

namespace Labb2API.DAL;

public class UserStorage
{
    private readonly IDictionary<string, User> _users;

    //private int _id;

    public UserStorage()
    {
        _users = new Dictionary<string, User>();
    }

    public bool CreateUser(User user)
    {
        if (_users.Keys.Contains(user.Email)) return false;

        _users.Add(user.Email, user);

        return true;
    }

    public ICollection<User>? GetAllUsers()
    {
        return _users.Values;
    }

    //Kollar om det finns en user med en email, returnerar om den finns.
    public User? GetUser(string email)
    {
        if (_users.ContainsKey(email)) return _users[email];

        return null;
    }

    //PUT? Ändrar hela användaren.
    public bool UpdateUser(User user)
    {
        if (!_users.Keys.Contains(user.Email)) return false;

        _users[user.Email] = user;

        return true;
    }

    //TODO Gör Add/Delete metoder istället för denna.
    public bool UpdateActiveCourses(string email, List<Course> courses)
    {
        if (courses.Count <= 0) return false;

        _users[email].ActiveCourses = courses;

        return true;
    }

    public bool DeleteUser(User user)
    {
        if (!_users.Keys.Contains(user.Email)) return false;

        _users.Remove(user.Email);

        return true;
    }
}