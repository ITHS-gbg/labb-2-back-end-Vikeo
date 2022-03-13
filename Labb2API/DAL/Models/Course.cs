using System.ComponentModel.DataAnnotations;
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

        public Course(string title, string description, int duration, CourseDifficulty difficulty, CourseStatus status)
        {
            //TODO Id blir unikt, men det 
            Id = Id++;
            Title = title;
            Description = description;
            Duration = duration;
            Difficulty = difficulty;
            Status = status;
        }
    }
}