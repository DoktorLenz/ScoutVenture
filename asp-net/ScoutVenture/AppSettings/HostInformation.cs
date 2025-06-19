namespace AppSettings
{
    public class HostInformation
    {
        public static string HostInformationKey { get; } = "HostInformation";
        
        public required string BaseUrl { get; init; }
    }
}