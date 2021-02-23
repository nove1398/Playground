using CustomAuthenticationFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthenticationFilter.Data
{
    public class SenitFxDbContext : DbContext
    {
        public SenitFxDbContext(DbContextOptions<SenitFxDbContext> options) : base(options)
        {
        }

        public DbSet<SenitFxKey> SenitFxKeys { get; set; }
    }
}