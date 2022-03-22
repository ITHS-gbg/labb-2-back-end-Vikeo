namespace Labb2API.DAL.Models;

public record User
{
    //TODO public Guid Id { get; set; }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    //Ta bort om du vill ha med kurser när man hämtar en users.

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
