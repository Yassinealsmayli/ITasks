using ITasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ITasks
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ITask> Tasks { get; set; }

        public DbSet<UserTask> userTasks { get; set; }

        public static User? currentUser { get; set; } 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
