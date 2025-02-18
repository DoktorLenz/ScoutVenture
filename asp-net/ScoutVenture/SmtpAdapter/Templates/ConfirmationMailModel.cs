namespace SmtpAdapter.Templates
{
    public class ConfirmationMailModel(string confirmationLink)
    {
        public string ConfirmationLink { get; init; } = confirmationLink;
    }
}