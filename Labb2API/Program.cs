using Labb2API.Backend.DAL;
using Labb2API.Backend.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseRoute")));

builder.Services.AddRazorPages();

//Detta ersätter OnConfiguring
builder.Services.AddDbContext<WebsiteContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("UserDbConnectionString");
    options.UseSqlServer(connectionString);
});

//TODO är detta rätt?
builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.Run();