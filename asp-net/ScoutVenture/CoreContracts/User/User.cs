namespace ScoutVenture.CoreContracts.User
{
    public class User
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public required IEnumerable<Member.Member> LinkedMembers { get; set; }
    }
}