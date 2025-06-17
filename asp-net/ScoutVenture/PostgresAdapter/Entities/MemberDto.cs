using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using ScoutVenture.CoreContracts.Member;

namespace ScoutVenture.PostgresAdapter.Entities
{
    public class MemberDto
    {
        
        [Key]
        public required long MemberId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }
        
        [Required]
        public required DateOnly DateOfBirth { get; set; }
        
        public Rank? Rank { get; set; }
        
        [Required]
        public required Gender Gender { get; set; }
        
        [Timestamp]
        public uint Version { get; set; }

        public static MemberDto FromDo(Member member)
        {
            return new MemberDto
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