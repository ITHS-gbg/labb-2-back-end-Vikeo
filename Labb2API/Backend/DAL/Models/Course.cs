using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Labb2API.Backend.DAL.Enums;

namespace Labb2API.Backend.DAL.Models;

public record Course : ISerializable
{
    [Key]
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("Title")]
    public string Title { get; set; }
    [JsonPropertyName("Description")]
    public string Description { get; set; }
    [JsonPropertyName("Duration")]
    public int Duration { get; set; }
    [JsonPropertyName("Difficulty")]
    public CourseDifficulty Difficulty { get; set; }
    //TODO Change name to IsActive, etc.
    [JsonPropertyName("Status")]
    public bool Status { get; set; }
    [JsonPropertyName("Users")]
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; }

    public Course(string title, string description, int duration, CourseDifficulty difficulty, bool status)
    {
        Title = title;
        Description = description;
        Duration = duration;
        Difficulty = difficulty;
        Status = status;
        Users = new HashSet<User>();
    }

    //Entity framework behöver denna när man ska hämta data från ActiveCourse.
    public Course()
    {
        Users = new HashSet<User>();
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        //Kommer inte ens in hit??
        //info.AddValue("Difficulty", (CourseDifficulty)int.Parse(info.GetString("Difficulty")));
        //info.AddValue("Status", (CourseDifficulty)int.Parse(info.GetString("Status")));
        //TODO Fixa så att den tolkar int som enum. Detta sker innan serialisering
    }
}
