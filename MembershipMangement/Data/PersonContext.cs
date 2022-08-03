using MembershipMangement.Models;
using Microsoft.EntityFrameworkCore;

namespace MembershipMangement.Data
{
    public class PersonContext: DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
    }
}
