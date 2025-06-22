namespace ScoutVenture.PostgresAdapter.Entities
{
    public class UserMemberLinkDpo
    {
        public required string UserId { get; set; }
        public required UserDpo User { get; set; }
        
        public long MemberId { get; set; }
        public required MemberDpo Member { get; set; }
        
        public required string CreatedById { get; set; }
        public required UserDpo CreatedBy { get; set; }
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}