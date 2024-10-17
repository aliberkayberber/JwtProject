using JwtExample.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtExample.Context
{
    public class JwtDbContext : DbContext
    {
        public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options) 
        {
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
