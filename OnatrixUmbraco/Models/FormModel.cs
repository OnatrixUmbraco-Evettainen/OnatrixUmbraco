namespace OnatrixUmbraco.Models;

public class FormModel
{
    public Dictionary<string, string> Fields { get; set; }
    public Dictionary<string, ValidationRule>? ValidationRules { get; set; }

    public FormModel()
    {
        Fields = [];
        ValidationRules = [];
    }
}

public class ValidationRule
{
    public bool IsRequired { get; set; }
    public string? Regex { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Signature { get; set; }
}

public class FieldError
{
    public string? FieldAlias { get; set; }
    public string? ErrorMessage { get; set; }
}