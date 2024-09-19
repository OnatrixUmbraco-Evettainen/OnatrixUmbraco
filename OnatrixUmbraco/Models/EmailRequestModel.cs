using System.ComponentModel.DataAnnotations;

namespace OnatrixUmbraco.Models;

public class EmailRequestModel
{
    [Required]
    public string To { get; set; } = null!;

    [Required]
    public string Subject { get; set; } = null!;

    [Required]
    public string HtmlBody { get; set; } = null!;

    [Required]
    public string PlainText { get; set; } = null!;
}
