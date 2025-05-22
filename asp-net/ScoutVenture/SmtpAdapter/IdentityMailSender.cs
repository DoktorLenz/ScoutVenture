using System.Text.RegularExpressions;
using AppSettings;
using Fluid;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Crypto.Parameters;
using SmtpAdapter.Templates;

namespace SmtpAdapter;

public class IdentityMailSender(IOptions<SmtpOptions> smtpOptions, IOptions<HostInformation> hostInformation) : IEmailSender<IdentityUser>
{
    private readonly SmtpOptions _smtpOptions = smtpOptions.Value;
    private readonly HostInformation _hostInformation = hostInformation.Value;


    public async Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {
        UriBuilder uriBuilder = new(_hostInformation.BaseUrl);
        uriBuilder.Path += uriBuilder.Path.EndsWith('/') ? "" : "/";
        uriBuilder.Path += "auth/confirmEmail";
        uriBuilder.Query = new Uri(confirmationLink).Query;
        confirmationLink = uriBuilder.ToString();
        
        var parser = new FluidParser();
        var model = new ConfirmationMailModel(confirmationLink);
        var source = IdentityMailTemplates.ConfirmationLinkTemplate;

        if (parser.TryParse(source, out var template, out var error))
        {
            var context = new TemplateContext(model);
            
            
            MimeMessage message = new();
            message.From.Add(new MailboxAddress("ScoutVenture", _smtpOptions.Email));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = "Willkommen in ScoutVenture - E-Mail Adresse bestätigen";
            var text = await  template.RenderAsync(context);
            message.Body = new TextPart("html")
            {
                Text = text
            };

            await SendMessageAsync(message);
        }
    }

    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public async Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        UriBuilder uriBuilder = new(_hostInformation.BaseUrl);
        uriBuilder.Path += uriBuilder.Path.EndsWith('/') ? "" : "/";
        uriBuilder.Path += "auth/resetPassword";
        uriBuilder.Query = "email=" + Uri.EscapeDataString(email);
        uriBuilder.Query += "&" + "resetCode=" + Uri.EscapeDataString(resetCode);
        
        var resetLink = uriBuilder.ToString();
        
        var parser = new FluidParser();
        var model = new ResetMailModel(resetLink);
        var source = IdentityMailTemplates.ResetLinkTemplate;

        if (parser.TryParse(source, out var template, out var error))
        {
            var context = new TemplateContext(model);
            
            
            MimeMessage message = new();
            message.From.Add(new MailboxAddress("ScoutVenture", _smtpOptions.Email));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = "ScoutVenture - Passwort zurücksetzen";
            var text = await template.RenderAsync(context);
            message.Body = new TextPart("html")
            {
                Text = text
            };

            await SendMessageAsync(message);
        }
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