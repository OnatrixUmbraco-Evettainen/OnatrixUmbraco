using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using OnatrixUmbraco.Models;
using System.Text;

namespace OnatrixUmbraco.Services;


public interface IEmailService
{
    Task<bool> SendSupportConfirmationEmailAsync(string email);
}

public class EmailService : IEmailService
{
    private readonly ServiceBusClient _serviceBusClient;

    public EmailService(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

    public async Task<bool> SendSupportConfirmationEmailAsync(string email)
    {
        try
        {
            var emailRequest = GenerateSupportConfirmEmail(email);
            if (emailRequest != null)
            {
                var sender = _serviceBusClient.CreateSender("email_request");
                await sender.SendMessageAsync(new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emailRequest))));
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
        return false;
    }

    private EmailRequestModel GenerateSupportConfirmEmail(string email)
    {
        return new EmailRequestModel
        {
            To = email,
            Subject = "We've Received Your Support Request",
            HtmlBody = $@"<html lang='en'>
                <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Newsletter Confirmation</title>
                </head>
                <body>
                    <div style='max-width: 600px; margin: 20px auto; padding: 20px; background-color: #ffffff;'>
                        <div style='background-color: #0046ae; color: white; padding: 10px 20px; text-align: center;'>
                            <h1>We've Received Your Request</h1>
                        </div>
                         <div style='padding: 20px;'>
                            <p>Hello, {email}</p>
                            <p>Thank you for reaching out to our support team. We’ve received your message and will get back to you as soon as possible.</p>
                            <p>If this request wasn’t made by you, please contact our support team immediately.</p>
                            <p>Thank you!<br>The Support Team</p>
                        </div>
                    </div>
                </body>
                </html>",
            PlainText = $"Hello {email}, Thank you for reaching out to our support team. We’ve received your message and will get back to you as soon as possible. If this request wasn’t made by you, please contact our support team immediately. Thank you! The Support Team"
        };

    }
}