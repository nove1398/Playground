using Microsoft.EntityFrameworkCore;
using SignalRServer.Models;

namespace SignalRServer
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClassRoom>(entity =>
           {
               entity.HasMany(t => t.Students)
               .WithOne(stu => stu.ClassRoom)
               .HasForeignKey(stu => stu.ClassRoomId)
               .OnDelete(DeleteBehavior.NoAction);
           });
        }

        public DbSet<Grade> Grade { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
    }
}