using Labb2API.DAL.Models;
using Labb2API.DAL.Storages;
using Microsoft.EntityFrameworkCore;

namespace Labb2API.DAL;

public class WebsiteContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<User> Users { get; set; }
    ////Junction table?
    //public DbSet<UserCourse> UsersCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=Vikeo-DESKTOP;Initial Catalog=Databases; Database=WebLabb2Db;Integrated Security=True;Pooling=False");
    }

}

