using MembershipMangement.Models;
using Microsoft.EntityFrameworkCore;

namespace MembershipMangement.Data
{
    public class MembershipContext : DbContext
    {
        public MembershipContext(DbContextOptions<MembershipContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Membership> Membership { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Membership>().HasKey(table => new {
                table.PersonId,
                table.Type
            });
        }
    }
}
