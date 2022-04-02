using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DotNetNinja.Dojo.Annotations;

/// <summary>
/// Must contain only letters (upper|lower), numbers and characters - . \ : and it cannot start or end with special character and must start with a letter.
/// </summary>
public class EntityNameAttribute: ValidationAttribute
{
    public const string ValidationExpression = @"^[a-zA-Z]{1,}[a-zA-Z0-9\-\.\:\/]{0,}[a-zA-Z0-9]{1,}$";

    public override bool RequiresValidationContext => false;

    public override bool IsValid(object? value)
    {
        return value is string text && Regex.IsMatch(text, ValidationExpression);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(IsValid(value)) return ValidationResult.Success;
        var message = FormatErrorMessage(validationContext.DisplayName);
        return new ValidationResult(message);
    }
}