using AppSettings;
using Fluid;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;
using ScoutVenture.PostgresAdapter.Entities;
using SmtpAdapter.Templates;

namespace SmtpAdapter
{
    public class IdentityMailSender(IOptions<SmtpOptions> smtpOptions, IOptions<HostInformation> hostInformation)
        : IEmailSender<UserDpo>
    {
        private readonly HostInformation _hostInformation = hostInformation.Value;
        private readonly SmtpOptions _smtpOptions = smtpOptions.Value;


        public async Task SendConfirmationLinkAsync(UserDpo user, string email, string confirmationLink)
        {
            UriBuilder uriBuilder = new(_hostInformation.BaseUrl);
            uriBuilder.Path += uriBuilder.Path.EndsWith('/') ? "" : "/";
            uriBuilder.Path += "auth/confirmEmail";
            uriBuilder.Query = new Uri(confirmationLink).Query;
            confirmationLink = uriBuilder.ToString();

            FluidParser parser = new();
            ConfirmationMailModel model = new(confirmationLink);
            string source = IdentityMailTemplates.ConfirmationLinkTemplate;

            if (parser.TryParse(source, out IFluidTemplate? template, out string? error))
            {
                TemplateContext context = new(model);


                MimeMessage message = new();
                message.From.Add(new MailboxAddress("ScoutVenture", _smtpOptions.Email));
                message.To.Add(new MailboxAddress(email, email));
                message.Subject = "Willkommen in ScoutVenture - E-Mail Adresse bestätigen";
                string? text = await template.RenderAsync(context);
                message.Body = new TextPart("html")
                {
                    Text = text
                };

                await SendMessageAsync(message);
            }
        }

        public Task SendPasswordResetLinkAsync(UserDpo user, string email, string resetLink)
        {
            throw new NotImplementedException();
        }

        public async Task SendPasswordResetCodeAsync(UserDpo user, string email, string resetCode)
        {
            UriBuilder uriBuilder = new(_hostInformation.BaseUrl);
            uriBuilder.Path += uriBuilder.Path.EndsWith('/') ? "" : "/";
            uriBuilder.Path += "auth/resetPassword";
            uriBuilder.Query = "email=" + Uri.EscapeDataString(email);
            uriBuilder.Query += "&" + "resetCode=" + Uri.EscapeDataString(resetCode);

            string resetLink = uriBuilder.ToString();

            FluidParser parser = new();
            ResetMailModel model = new(resetLink);
            string source = IdentityMailTemplates.ResetLinkTemplate;

            if (parser.TryParse(source, out IFluidTemplate? template, out string? error))
            {
                TemplateContext context = new(model);


                MimeMessage message = new();
                message.From.Add(new MailboxAddress("ScoutVenture", _smtpOptions.Email));
                message.To.Add(new MailboxAddress(email, email));
                message.Subject = "ScoutVenture - Passwort zurücksetzen";
                string? text = await template.RenderAsync(context);
                message.Body = new TextPart("html")
                {
                    Text = text
                };

                await SendMessageAsync(message);
            }
        }

        private async Task SendMessageAsync(MimeMessage message)
        {
            using SmtpClient client = new();
            await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.EnableSsl);
            await client.AuthenticateAsync(_smtpOptions.Email, _smtpOptions.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}