using System.Text.Json.Serialization;

namespace Labb2API.DAL.Models
{
    public class UserCourse
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }

        public UserCourse()
        {
            Users = new HashSet<User>();
            Courses = new HashSet<Course>();
        }
    }
}
