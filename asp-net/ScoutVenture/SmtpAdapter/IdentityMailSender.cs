using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;

namespace SmtpAdapter;

public class IdentityMailSender(IOptions<SmtpOptions> options) : IEmailSender<IdentityUser>
{
    private readonly SmtpOptions _smtpOptions = options.Value;


    public async Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {
        MimeMessage message = new();
        message.From.Add(new MailboxAddress("ScoutVenture", _smtpOptions.Email));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = "Willkommen in ScoutVenture - E-Mail Adresse bestätigen";
        message.Body = new TextPart("html")
        {
            Text = $"""
                    Klicken Sie auf den Link um ihre Anmeldung bei ScoutVenture zu bestätigen.
                    <a href="{confirmationLink}">Link</a>
                    """
        };

        await SendMessageAsync(message);
    }

    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }

    private async Task SendMessageAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.EnableSsl);
        await client.AuthenticateAsync(_smtpOptions.Email, _smtpOptions.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}