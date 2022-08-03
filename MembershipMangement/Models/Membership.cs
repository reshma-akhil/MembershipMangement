using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MembershipMangement.Models
{
    public enum MembershipType
    {
        None,
        Primary,
        Secondary
    }

    public class Membership
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "Select member")]
        [Key]
        [ForeignKey("Person")]
        [Column(Order = 1)]
        public int PersonId { get; set; }
        [Required]
        [StringLength(20)]
        public string Number { get; set; }
        [Key]
        [Column(Order = 2)]
        [Required, Range(1, int.MaxValue, ErrorMessage = "Select MembershipType")]
        public MembershipType Type { get; set; } = MembershipType.Primary;
        [Range(0, 999.99)]
        public decimal Balance { get; set; }
    }
}
