namespace ScoutVenture.PostgresAdapter.Entities
{
    public class UserMemberLinkDpo
    {
        public required string UserId { get; set; }
        public UserDpo User { get; set; }
        
        public long MemberId { get; set; }
        public MemberDpo Member { get; set; }
        
        public required string CreatedById { get; set; }
        public UserDpo CreatedBy { get; set; }
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}