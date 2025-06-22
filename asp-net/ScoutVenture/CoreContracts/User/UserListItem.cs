namespace ScoutVenture.CoreContracts.User
{
    public class UserListItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int LinkedMemberCount { get; set; }
    }
}