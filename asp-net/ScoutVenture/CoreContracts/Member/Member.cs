using NamiClient;

namespace ScoutVenture.CoreContracts.Member
{
    public class Member
    {
        public required long MemberId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public Rank? Rank { get; set; }
        public required Gender Gender { get; set; }

        public static Member FromNami(NamiMember namiMember)
        {
            var rank = EnumParser.ParseRank(namiMember.Rank);
            var gender = EnumParser.ParseGender(namiMember.Gender);
            
            return new Member
            {
                MemberId = namiMember.MemberId,
                FirstName = namiMember.FirstName,
                LastName = namiMember.LastName,
                DateOfBirth = namiMember.DateOfBirth,
                Rank = rank,
                Gender = gender
            };
        }
    }
}
