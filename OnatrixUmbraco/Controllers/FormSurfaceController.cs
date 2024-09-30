using Azure.Messaging.ServiceBus;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnatrixUmbraco.Helpers;
using OnatrixUmbraco.Services;
using OnatrixUmbraco.ViewModels;
using System.Text;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;

using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;


namespace OnatrixUmbraco.Controllers;

public class FormSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IPublishedContentQuery contentQuery, Signature signature, IFormValidationService formValidationService, IEmailService emailService, ServiceBusClient serviceBusclient, FormSubmissionService formSubmissionService) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    private readonly Signature _signature = signature;
    private readonly IFormValidationService _formValidationService = formValidationService;
    private readonly IEmailService _emailService = emailService;
    private readonly ServiceBusClient _serviceBusclient = serviceBusclient;
    private readonly FormSubmissionService _formSubmissionService = formSubmissionService;

   

    [HttpPost]
    public async Task<IActionResult> HandleCallBackForm(FormCallBackViewModel form)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var saveSubmission = _formSubmissionService.SaveRequestsForm(form);
        if (!saveSubmission)
        {
            TempData["Error"] = "An error occurred while sending the email. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }


        var emailSent = await _emailService.SendRequestConfirmationEmailAsync(form.Email, form.SelectedOption);
        if (emailSent)
        {
            TempData["Success"] = "Thanks for your request! An email has been sent to your inbox. We are working to assist you as quickly as we can.";
        }
        else
        {
            TempData["Error"] = "An error occurred while sending the email. Please try again later.";
        }
        return RedirectToCurrentUmbracoPage();
    }


    [HttpPost]
    public async Task<IActionResult> HandleContactForm(FormContactViewModel form)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var emailSent = await _emailService.SendRequestConfirmationEmailAsync(form.Email, form.SelectedOption);
        if (emailSent)
        {
            TempData["Success"] = "Thanks for your request! An email has been sent to your inbox. We are working to assist you as quickly as we can.";
        }
        else
        {
            TempData["Error"] = "An error occurred while sending the email. Please try again later.";
        }
        return RedirectToCurrentUmbracoPage();
    }


    [HttpPost]
    public async Task<IActionResult> HandleSupportForm(FormSupportViewModel form)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var emailSent = await _emailService.SendSupportConfirmationEmailAsync(form.Email);
        if (emailSent)
        {
            ViewData["Success"] = "Thanks for your request! An email has been sent to your inbox. We are working to assist you as quickly as we can.";
        }
        else
        {
            ViewData["Error"] = "An error occurred while sending the email. Please try again later.";
        }
        return RedirectToCurrentUmbracoPage();
    }

    

    [HttpPost]
    public async Task<IActionResult> HandleQuestionForm(FormQuestionViewModel form)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var emailSent = await _emailService.SendQuestionConfirmationEmailAsync(form.Email, form.Question);
        if (emailSent)
        {
            ViewData["Success"] = "Thanks for your request! An email has been sent to your inbox. We are working to assist you as quickly as we can.";
        }
        else
        {
            ViewData["Error"] = "An error occurred while sending the email. Please try again later.";
        }
        return RedirectToCurrentUmbracoPage();
    }
}
