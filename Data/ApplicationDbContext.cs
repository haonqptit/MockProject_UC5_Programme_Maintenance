using Microsoft.EntityFrameworkCore;
using MockProject.Models;

namespace MockProject.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Programme> Programmes { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
}