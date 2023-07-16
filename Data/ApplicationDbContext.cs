using Innoloft.Models;
using Microsoft.EntityFrameworkCore;

namespace Innoloft.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        
    }
}