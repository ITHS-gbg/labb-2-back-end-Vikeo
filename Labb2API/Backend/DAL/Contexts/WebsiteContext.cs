using Labb2API.Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb2API.Backend.DAL.Contexts;

//När jag ska skapa databasen, skriv "add-migration <namn på migrationen>", sen för att skapa "Update-database"
public class WebsiteContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<User> Users { get; set; }

    public WebsiteContext(DbContextOptions options) : base(options)
    {

    }
}

