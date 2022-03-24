using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb2API.Backend.DAL.Models;

public record User
{
    //TODO public Guid Id { get; set; } ?????
    [JsonPropertyName("Id")]
    [Key]
    public int Id { get; set; }
    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("LastName")]
    public string LastName { get; set; }

    //TODO Can I set Email to be unique here in anyway?
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    [JsonPropertyName("Email")]
    public string Email { get; set; }
    [JsonPropertyName("Phone")]
    public string Phone { get; set; }
    [JsonPropertyName("Address")]
    public string Address { get; set; }
    [JsonPropertyName("ActiveCourses")]

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
