// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

class User {
    constructor(firstName, lastName, email, phone, address, activeCourses) {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
        this.Address = address;
        this.ActiveCourses = activeCourses;
    }
}

class Course {
    constructor(id, title, description, duration, difficulty, status) {
        this.Id = id;
        this.Title = title;
        this.Description = description;
        this.Duration = duration;
        this.Difficulty = difficulty;
        this.Status = status;
    }
}