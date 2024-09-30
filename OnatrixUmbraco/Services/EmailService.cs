using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using OnatrixUmbraco.Models;
using Polly.CircuitBreaker;
using System.Text;
using static Umbraco.Cms.Core.Diagnostics.MiniDump;

namespace OnatrixUmbraco.Services;


public interface IEmailService
{
  
    Task<bool> SendRequestConfirmationEmailAsync(string email, string option);
    Task<bool> SendQuestionConfirmationEmailAsync(string email, string question);
    Task<bool> SendSupportConfirmationEmailAsync(string email);
}

public class EmailService : IEmailService
{
    private readonly ServiceBusClient _serviceBusClient;

    public EmailService(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }


    public async Task<bool> SendRequestConfirmationEmailAsync(string email, string option)
    {
        try
        {
            var emailRequest = GenerateRequestConfirmEmail(email, option);
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

    private EmailRequestModel GenerateRequestConfirmEmail(string email, string option)
    {
        var optionMessage = GetOptionMessage(option);

        return new EmailRequestModel
        {
            To = email,
            Subject = "We've Received Your Request",
            HtmlBody = $@"<html lang='en'>
                <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Request Confirmation</title>
                </head>
                <body>
                    <div style='max-width: 600px; margin: 20px auto; padding: 20px; background-color: #ffffff;'>
                        <div style='background-color: #0046ae; color: white; padding: 10px 20px; text-align: center;'>
                            <h1>We've Received Your Request</h1>
                        </div>
                         <div style='padding: 20px;'>
                            <p>Hello, {email}</p>
                            <p>{optionMessage}</p>
                            <p>If this request wasn’t made by you, please contact our support team immediately.</p>
                            <p>Thank you!<br>The Support Team</p>
                        </div>
                    </div>
                </body>
                </html>",
            PlainText = $"Hello {email}, {optionMessage}. If this request wasn’t made by you, please contact our support team immediately. Thank you! The Support Team"
        };

    }

    private string GetOptionMessage(string option)
    {
        switch (option)
        {
            case "Financial consulting":
                return "We’ve noted that you selected Financial Consulting. Our experts will prioritize your request to ensure the best financial advice and support.";
            case "Business strategy":
                return "Thank you for selecting Business Strategy. Our team will focus on providing tailored solutions to help you achieve your business goals.";
            case "Tax planning":
                return "You’ve chosen Tax Planning. Our support team is prepared to assist you with comprehensive tax strategies to optimize your financial situation.";
            default:
                return "We’ve received your request and will process it based on the information provided.";
        }
    }

    public async Task<bool> SendQuestionConfirmationEmailAsync(string email, string question)
    {
        try
        {
            var emailRequest = GenerateQuestionConfirmEmail(email, question);
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

    private EmailRequestModel GenerateQuestionConfirmEmail(string email, string question)
    {
        return new EmailRequestModel
        {
            To = email,
            Subject = "We've Received Your Question",
            HtmlBody = $@"<html lang='en'>
                <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Question Confirmation</title>
                </head>
                <body>
                    <div style='max-width: 600px; margin: 20px auto; padding: 20px; background-color: #ffffff;'>
                        <div style='background-color: #0046ae; color: white; padding: 10px 20px; text-align: center;'>
                            <h1>We've Received Your Question</h1>
                        </div>
                         <div style='padding: 20px;'>
                            <p>Hello, {email}</p>
                            <p>Thank you for reaching out. We’ve received your question:</p>
                            <p>{question}</p> 
                            <p>We will get back to you as soon as possible.</p>
                            <p>If this request wasn’t made by you, please contact our support team immediately.</p>
                            <p>Thank you!<br>The Support Team</p>
                        </div>
                    </div>
                </body>
                </html>",
            PlainText = $"Hello {email}, Thank you for reaching out to our support team. We’ve received your question: {question}. We will get back to you as soon as possible. If this request wasn’t made by you, please contact our support team immediately. Thank you! The Support Team"
        };

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
                <title>Support Confirmation</title>
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