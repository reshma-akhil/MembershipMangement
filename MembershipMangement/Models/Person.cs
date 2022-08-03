using System.ComponentModel.DataAnnotations;

namespace MembershipMangement.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string EmailId { get; set; }

    }
}
