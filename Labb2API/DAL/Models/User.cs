using System.ComponentModel.DataAnnotations;

namespace Labb2API.DAL.Models;

public record User
{
    //TODO public Guid Id { get; set; } ?????
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    //TODO Can I set Email to be unique here in anyway?
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    //[JsonIgnore] - Add if don't want it to be included when serializing users.
    public virtual ICollection<Course> ActiveCourses { get; set; }

    public User(string firstName, string lastName, string email, string phone, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        ActiveCourses = new HashSet<Course>();
    }

    //Entity framework behöver denna när man ska hämta data från ActiveCourse.
    public User()
    {
        ActiveCourses = new HashSet<Course>();
    }
}
