using Microsoft.EntityFrameworkCore;
using SignalRServer.Models;

namespace SignalRServer
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Grade> Grade { get; set; }
        public DbSet<Student> Student { get; set; }
    }
}