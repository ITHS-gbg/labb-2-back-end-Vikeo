using Labb2API.DAL.Models;
using Labb2API.DAL.Storages;
using Microsoft.EntityFrameworkCore;

namespace Labb2API.DAL;

//När jag ska skapa databasen, skriv "add-migration <namn på migrationen>", sen för att skapa "Update-database"
public class WebsiteContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<User> Users { get; set; }
    ////Junction table?
    //public DbSet<UserCourse> UsersCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Desktop
        //optionsBuilder.UseSqlServer(@"Data Source=Vikeo-DESKTOP;Initial Catalog=Databases; Database=WebLabb2Db;Integrated Security=True;Pooling=False");
        //Laptop
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-HMMNO25;Initial Catalog=Databases; Database=WebLabb2Db;Integrated Security=True;Pooling=False");

    }

}

