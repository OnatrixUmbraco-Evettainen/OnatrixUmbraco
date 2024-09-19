using MessagePack.Formatters;
using System.ComponentModel.DataAnnotations;

namespace OnatrixUmbraco.ViewModels;

public class SupportFormModel
{
    [Required]

    public string Email { get; set; } = null!;

}