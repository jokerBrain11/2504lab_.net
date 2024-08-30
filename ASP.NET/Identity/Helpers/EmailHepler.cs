using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailHepler : IEmailSender
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromAddress;

    public EmailHepler(IConfiguration configuration)
    {
        var smtpServer = configuration["Smtp:Server"];
        var port = int.Parse(configuration["Smtp:Port"]);
        var fromAddress = configuration["Smtp:FromAddress"];
        var username = configuration["Smtp:Username"];
        var password = configuration["Smtp:Password"];

        _fromAddress = fromAddress;
        _smtpClient = new SmtpClient(smtpServer, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_fromAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        await _smtpClient.SendMailAsync(mailMessage);
    }
}
