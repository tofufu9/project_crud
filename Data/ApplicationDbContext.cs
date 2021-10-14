using Microsoft.EntityFrameworkCore;
using project_crud.Models;

namespace project_crud.Data
{
    public class ApplicationDbContext : DbContext
    {
    // đặt DbSet cho nhóm Contact
        public virtual DbSet<Contact> Contacts { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)    
    {
        base.OnModelCreating(modelBuilder);
    }
    }
}