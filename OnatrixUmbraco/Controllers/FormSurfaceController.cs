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

public class FormSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IPublishedContentQuery contentQuery, Signature signature, IFormValidationService formValidationService, IEmailService emailService, ServiceBusClient serviceBusclient) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    private readonly Signature _signature = signature;
    private readonly IFormValidationService _formValidationService = formValidationService;
    private readonly IEmailService _emailService = emailService;
    private readonly ServiceBusClient _serviceBusclient = serviceBusclient;

    [HttpPost]
    public async Task<IActionResult> HandleSubmit(FormModel model)
    {
        if (!_formValidationService.ValidateForm(model, out var errors))
        {
            foreach (var error in errors)
            {
                ViewData[$"{error.FieldAlias}Error"] = error.ErrorMessage;
            }

            foreach (var field in model.Fields)
            {
                if (!errors.Any(e => e.FieldAlias == field.Key))
                {
                    ViewData[$"{field.Key}IsValid"] = true;
                }
                ViewData[field.Key] = field.Value;
            }
            return CurrentUmbracoPage();
        }



        if (model.Fields.TryGetValue("email", out var email) && !string.IsNullOrEmpty(email))
        {
            var emailSent = await _emailService.SendSupportConfirmationEmailAsync(email);
            if (!emailSent)
            {
                TempData["Error"] = "An error occurred while sending the email. Please try again later.";
            }

            
        }
        TempData["Success"] = "Thanks for your request! An email has been sent to your inbox. We’re working to assist you as quickly as we can.";
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
        if (!emailSent)
        {
            TempData["Error"] = "An error occurred while sending the email. Please try again later.";
        }

        TempData["Success"] = "Thanks for your request! An email has been sent to your inbox. We’re working to assist you as quickly as we can.";
        return RedirectToCurrentUmbracoPage();
    }

}
