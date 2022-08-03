using MembershipMangement.Models;
using Microsoft.EntityFrameworkCore;

namespace MembershipMangement.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MembershipContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MembershipContext>>()))
            {
                // Look for any movies.
                if (context.Membership.Any())
                {
                    return;   // DB has been seeded
                }

                context.Membership.AddRange(
                    new Membership
                    {
                        PersonId = 1,
                        Number = "M0001",
                        Type = MembershipType.Primary,
                        Balance = 100
                    },
                    new Membership
                    {
                        PersonId = 1,
                        Number = "M0002",
                        Type = MembershipType.Secondary,
                        Balance = 50
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
