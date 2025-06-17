using NamiClient;

namespace ScoutVenture.CoreContracts.Member
{
    public class Member
    {
        public required long MemberId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required Rank Rank { get; set; }
        public required Gender Gender { get; set; }

        public static Member FromNami(NamiMember namiMember)
        {
            Rank rank = EnumParser.ParseRank(namiMember.Rank);
            Gender gender = EnumParser.ParseGender(namiMember.Gender);

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