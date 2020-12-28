using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Voting> Votings { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<User>()
                .HasMany(u => u.Votings)
                .WithMany(v => v.Users)
                .UsingEntity<UsersVotings>(
                j => j.HasOne(m => m.Voting).WithMany(g => g.UsersVotings), j => j.HasOne(m => m.User).WithMany(u => u.UsersVotings));
        }

    }
    #endregion
}