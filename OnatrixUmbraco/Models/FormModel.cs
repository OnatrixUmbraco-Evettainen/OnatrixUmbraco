using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.RegularExpressions;

namespace OnatrixUmbraco.Models;

public class FormModel
{
    public Dictionary<string, string>? Fields { get; set; }
    public Dictionary<string, ValidationRule>? ValidationRules { get; set; }

    public FormModel()
    {
        Fields = new Dictionary<string, string>();
        ValidationRules = new Dictionary<string, ValidationRule>();
    }

    public bool IsValid(out List<string> errors)
    {
        errors = new List<string>();

        if (Fields != null && Fields.Any())
        {
            foreach (var field in Fields)
            {
                var fieldName = field.Key;
                var fieldValue = field.Value;

                if (ValidationRules.ContainsKey(fieldName))
                {
                    var validationRule = ValidationRules[fieldName];

                    if (validationRule.IsRequired && string.IsNullOrEmpty(fieldValue))
                    {
                        errors.Add($"{fieldName} is required.");
                        continue;
                    }

                    if (!string.IsNullOrEmpty(validationRule.Regex))
                    {
                        var regex = new Regex(validationRule.Regex);
                        if (!regex.IsMatch(fieldValue))
                        {
                            errors.Add($"{fieldName} is invalid.");
                        }
                    }
                }
            }

            
        }
        return errors.Count == 0;

    }

}






public class ValidationRule
{
    public bool IsRequired { get; set; }
    public string? Regex { get; set; }
    public string? ErrorMessage { get; set; }
}