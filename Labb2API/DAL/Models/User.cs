﻿namespace Labb2API.DAL.Models
{
    public record User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Course> ActiveCourses { get; set; }

        public User(string firstName, string lastName, string email, string phone, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            ActiveCourses = new List<Course>();
        }
    }
}