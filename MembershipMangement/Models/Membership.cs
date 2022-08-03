using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembershipMangement.Models
{
    public enum MembershipType
    {
        Primary,
        Secondary
    }

    public class Membership
    {
       //[Key]
        //public int Id { get; set; }
        [Key]
        [ForeignKey("Person")]
        [Column(Order = 1)]
        public int PersonId { get; set; }
        public string Number { get; set; }
        [Key]
        [Column(Order = 2)]
        public MembershipType Type { get; set; } = MembershipType.Primary;
        public decimal Balance { get; set; }
    }
}
