using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Labb2API.DAL.Enums;

namespace Labb2API.DAL.Models
{
    public record Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public CourseDifficulty Difficulty { get; set; }
        public CourseStatus Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }

        public Course(string title, string description, int duration, CourseDifficulty difficulty, CourseStatus status)
        {
            //TODO Id blir inte unikt vad bara detta.
            Id = Id++;
            Title = title;
            Description = description;
            Duration = duration;
            Difficulty = difficulty;
            Status = status;
            Users = new List<User>();
        }

        //Entity framework behöver denna när man ska hämta data från ActiveCourse.
        public Course()
        {
            Users = new List<User>();
        }
    }
}