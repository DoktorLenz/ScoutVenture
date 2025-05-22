namespace SmtpAdapter.Templates
{
    public class ResetMailModel (string resetLink)
    {
        public string ResetLink { get; init; } = resetLink;
    }
}