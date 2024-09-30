﻿using System.ComponentModel.DataAnnotations;

namespace OnatrixUmbraco.ViewModels;

public class FormQuestionViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email address is required")]
    [RegularExpression(@"^(([^<>()\]\\.,;:\s@\""]+(\.[^<>()\]\\.,;:\s@\""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Question is required")]
    public string Question { get; set; } = null!;
}
