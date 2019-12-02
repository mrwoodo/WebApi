using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApi.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class WhitelistCharacterAttribute : ValidationAttribute
    {
        const string regExPattern = @"^([\-\.,0-9a-zA-Z ]+)$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var input = value as string;

                if (Regex.IsMatch(input, regExPattern))
                    return ValidationResult.Success;

                return new ValidationResult("Invalid character(s) present", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
