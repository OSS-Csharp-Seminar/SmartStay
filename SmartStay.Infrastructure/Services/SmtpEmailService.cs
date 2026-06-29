using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using SmartStay.Application.Interfaces;

namespace SmartStay.Infrastructure.Services;

public class SmtpEmailService : IEmailService
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;

    public SmtpEmailService(IConfiguration config)
    {
        _host = config["Smtp:Host"]!;
        _port = int.Parse(config["Smtp:Port"]!);
        _username = config["Smtp:Username"]!;
        _password = config["Smtp:Password"]!;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_host, _port)
        {
            Credentials = new NetworkCredential(_username, _password),
            EnableSsl = true
        };

        var message = new MailMessage(
            from: _username,
            to: to,
            subject: subject,
            body: body);

        await client.SendMailAsync(message);
    }
}
