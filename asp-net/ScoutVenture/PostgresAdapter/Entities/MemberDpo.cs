using System.ComponentModel.DataAnnotations;
using ScoutVenture.CoreContracts.Member;

namespace ScoutVenture.PostgresAdapter.Entities
{
    public class MemberDpo
    {
        [Key] public required long MemberId { get; set; }

        [Required] [MaxLength(50)] public required string FirstName { get; set; }

        [Required] [MaxLength(100)] public required string LastName { get; set; }

        [Required] public required DateOnly DateOfBirth { get; set; }

        [Required] public required Rank Rank { get; set; }

        [Required] public required Gender Gender { get; set; }

        [Timestamp] public uint Version { get; set; }

        public static MemberDpo FromDo(Member member)
        {
            return new MemberDpo
            {
                MemberId = member.MemberId,
                FirstName = member.FirstName,
                LastName = member.LastName,
                DateOfBirth = member.DateOfBirth,
                Rank = member.Rank,
                Gender = member.Gender
            };
        }

        public void ApplyUpdate(Member member)
        {
            if (member.MemberId != MemberId)
            {
                throw new ArgumentException("MemberId mismatch");
            }

            FirstName = member.FirstName;
            LastName = member.LastName;
            DateOfBirth = member.DateOfBirth;
            Rank = member.Rank;
            Gender = member.Gender;
        }
    }
}