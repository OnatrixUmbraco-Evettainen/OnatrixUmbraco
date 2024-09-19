using OnatrixUmbraco.Helpers;
using OnatrixUmbraco.ViewModels;
using System.Text.RegularExpressions;

namespace OnatrixUmbraco.Services;

public interface IFormValidationService
{
    bool ValidateForm(FormModel model, out List<FieldError> errors);
}


public class FormValidationService(Signature signature) : IFormValidationService
{
    private readonly Signature _signature = signature;

    public bool ValidateForm(FormModel model, out List<FieldError> errors)
    {
        errors = [];

        foreach (var field in model.Fields)
        {
            var fieldAlias = field.Key;
            var fieldValue = field.Value;

           
            if (model.ValidationRules != null && model.ValidationRules.TryGetValue(fieldAlias, out ValidationRule? value))
            {
                var validationRule = value;

               
                var clientSignature = validationRule.Signature;
                var generatedSignature = _signature.GenerateSignature(fieldAlias, validationRule.IsRequired, validationRule.Regex ?? "");

                if (clientSignature != generatedSignature)
                {
                    errors.Add(new FieldError { FieldAlias = fieldAlias, ErrorMessage = "An unexpected error occurred, Please try again!" });
                    return false; 
                }

                
                if (validationRule.IsRequired && string.IsNullOrEmpty(fieldValue))
                {
                    var errorMessage = !string.IsNullOrEmpty(validationRule.RequiredMessage)
                        ? validationRule.RequiredMessage
                        : $"{char.ToUpper(fieldAlias[0])}{fieldAlias.Substring(1)} is required.";
                    errors.Add(new FieldError { FieldAlias = fieldAlias, ErrorMessage = errorMessage });
                }

                if (!string.IsNullOrEmpty(validationRule.Regex) && !string.IsNullOrEmpty(fieldValue))
                {
                    var regex = new Regex(validationRule.Regex);
                    if (!regex.IsMatch(fieldValue))
                    {
                        var errorMessage = !string.IsNullOrEmpty(validationRule.ExpressionMessage)
                            ? validationRule.ExpressionMessage
                            : $"{char.ToUpper(fieldAlias[0])}{fieldAlias.Substring(1)} is invalid.";
                        errors.Add(new FieldError { FieldAlias = fieldAlias, ErrorMessage = errorMessage });
                    }
                }
            }
        }

        return errors.Count == 0; 
    }
}
