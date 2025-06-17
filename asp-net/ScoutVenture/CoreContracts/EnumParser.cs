using ScoutVenture.CoreContracts.Member;

namespace ScoutVenture.CoreContracts
{
    public static class EnumParser
    {
        public static Rank ParseRank(string rank)
        {
            return rank switch
            {
                "Wölfling" => Rank.Woelfling,
                "Jungpfadfinder" => Rank.Jungpfadfinder,
                "Pfadfinder" => Rank.Pfadi,
                "Rover" => Rank.Rover,
                _ => Rank.None
            };
        }

        public static Gender ParseGender(string gender)
        {
            return gender switch
            {
                "männlich" => Gender.Male,
                "weiblich" => Gender.Female,
                _ => Gender.Divers
            };
        }
    }
}