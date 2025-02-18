namespace SmtpAdapter.Templates
{
    public static class IdentityMailTemplates
    {
        public const string ConfirmationLinkTemplate = """
                                                       <!DOCTYPE html>
                                                       <html>
                                                         <body style="margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f4f4f4; color: white;">
                                                           <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="background-color: #f4f4f4;">
                                                             <tr>
                                                               <td align="center">
                                                                 <table role="presentation" width="600" cellspacing="0" cellpadding="20" border="0" style="background-color: #003056; border-radius: 10px; text-align: center;">
                                                                   <tr>
                                                                     <td>
                                                                       <h1 style="margin: 0; font-size: 24px;">ScoutVenture</h1>
                                                                     </td>
                                                                   </tr>
                                                                   <tr>
                                                                     <td>
                                                                       <h3 style="margin: 0;">Willkommen bei ScoutVenture - Registrierung bestätigen</h3>
                                                                     </td>
                                                                   </tr>
                                                                   <tr>
                                                                     <td>
                                                                       <p style="margin: 0; font-size: 16px; line-height: 1.5;">
                                                                         Bevor du dein Konto benutzen kannst, musst du noch deine E-Mail-Adresse bestätigen.
                                                                       </p>
                                                                     </td>
                                                                   </tr>
                                                                   <tr>
                                                                     <td>
                                                                       <a href="{{ ConfirmationLink }}" style="display: inline-block; text-decoration: none; background-color: #003056; color: white; font-weight: bold; border: 1px solid white; padding: 10px 20px; border-radius: 4px; font-size: 16px;">
                                                                         E-Mail Adresse bestätigen
                                                                       </a>
                                                                     </td>
                                                                   </tr>
                                                                   <tr>
                                                                     <td>
                                                                       <p style="margin: 0; font-size: 12px; color: #ccc;">
                                                                         Du warst das nicht? Dann brauchst du nichts tun.
                                                                       </p>
                                                                     </td>
                                                                   </tr>
                                                                 </table>
                                                               </td>
                                                             </tr>
                                                           </table>
                                                         </body>
                                                       </html>
                                                       """;
    }
}