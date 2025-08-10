using ITasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ITasks
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ITask> Tasks { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }

        public DbSet<Admins> Admins { get; set; }
    }
}
