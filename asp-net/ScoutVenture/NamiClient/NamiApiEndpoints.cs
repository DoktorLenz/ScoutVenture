namespace NamiClient
{
    public static class NamiApiEndpoints
    {
        public const string Login = "ica/rest/nami/auth/manual/sessionStartup";

        public static string AllMembersOfGrouping(String groupingId)
        {
            return $"ica/rest/nami/mitglied/filtered-for-navigation/gruppierung/gruppierung/{groupingId}/flist";
        }
    }
}