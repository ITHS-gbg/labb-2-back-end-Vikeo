using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Labb2API.DAL.Models
{
    public record User
    {
        //TODO public Guid Id { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public virtual ICollection<Course> ActiveCourses { get; set; }

        public User(string firstName, string lastName, string email, string phone, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            ActiveCourses = new List<Course>();
        }

        public User(string firstName, string lastName, string email, string phone, string address, List<Course> activeCourses)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            ActiveCourses = activeCourses;
        }

        //Entity framework behöver denna när man ska hämta data från ActiveCourse.
        public User()
        {
            ActiveCourses = new List<Course>();
        }
    }
}