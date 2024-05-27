using Microsoft.EntityFrameworkCore;
using UserModule.Model;

namespace UserModule.Database
{
    public class UserDB(DbContextOptions<UserDB> options) : DbContext(options)
    {
        public DbSet<UserProfileModel> UserProfileModel { get; set; }
        public DbSet<UserLoginModel> UserLoginModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
