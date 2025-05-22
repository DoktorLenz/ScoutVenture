namespace AppSettings
{
    public class SmtpOptions
    {
        public static string SmtpOptionsKey { get; } = "SmtpOptions";
        
        public required string Host { get; init; }
        public int Port { get; init; }
        public bool EnableSsl { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}