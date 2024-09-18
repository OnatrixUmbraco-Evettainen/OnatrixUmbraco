using Microsoft.AspNetCore.Mvc;
using OnatrixUmbraco.Helpers;
using OnatrixUmbraco.Models;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;

using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;


namespace OnatrixUmbraco.Controllers;

public class FormSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IPublishedContentQuery contentQuery, Signature signature) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{

    private readonly Signature _signature = signature;

    [HttpPost]
    public IActionResult HandleSubmit(FormModel model)
    {
        var errors = new List<FieldError>();

        foreach (var field in model.Fields)
        {
            var fieldAlias = field.Key;
            var fieldValue = field.Value;

     
            if (model.ValidationRules != null && model.ValidationRules.TryGetValue(fieldAlias, out ValidationRule? value))
            {
                var validationRule = value;


                var clientSignature = validationRule.Signature;


                var generatedSignature = _signature.GenerateSignature(
                    fieldAlias,
                    validationRule.IsRequired,
                    validationRule.Regex ?? ""
                );


                if (clientSignature != generatedSignature)
                {
                    TempData["Error"] = "An unexpected error occurred, Please try again!";
                    return CurrentUmbracoPage();
                }


                if (validationRule.IsRequired && string.IsNullOrEmpty(fieldValue))
                {
                    var errorMessage = !string.IsNullOrEmpty(value.RequiredMessage)
                    ? value.RequiredMessage
                    : $"{char.ToUpper(fieldAlias[0])}{fieldAlias.Substring(1)} is required.";

                    errors.Add(new FieldError { FieldAlias = fieldAlias, ErrorMessage = errorMessage });
                }


                if (!string.IsNullOrEmpty(validationRule.Regex) && !string.IsNullOrEmpty(fieldValue))
                {
                    var regex = new Regex(validationRule.Regex);
                    if (!regex.IsMatch(fieldValue))
                    {
                        var errorMessage = !string.IsNullOrEmpty(value.ExpressionMessage)
                       ? value.ErrorMessage
                       : $"{char.ToUpper(fieldAlias[0])}{fieldAlias.Substring(1)} is invalid.";

                        errors.Add(new FieldError { FieldAlias = fieldAlias, ErrorMessage = errorMessage });
                    }
                }
            }
        }

        if (errors.Any())
        {
            foreach (var error in errors)
            {
                ViewData[$"{error.FieldAlias}Error"] = error.ErrorMessage;
            }

            foreach (var field in model.Fields)
            {
                if(!errors.Any(e => e.FieldAlias == field.Key))
                {
                    ViewData[$"{field.Key}IsValid"] = true;
                }

                ViewData[field.Key] = field.Value;
            }

            return CurrentUmbracoPage();
        }

        TempData["Success"] = "Hello World";
        return RedirectToCurrentUmbracoPage();

    }


    [HttpPost]
    public IActionResult HandleSupportForm(SupportFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        return RedirectToCurrentUmbracoPage();
    }

}
