namespace MembershipMangement.Models
{
    public class MembershipModel
    {
        public int MembershipId { get; set; }
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string EmailId { get; set; }
        public string Number { get; set; }
        public MembershipType Type { get; set; } = MembershipType.Primary;
        public decimal Balance { get; set; }
    }
}
