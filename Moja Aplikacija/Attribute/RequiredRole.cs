using Moja_Aplikacija.Entity;
using Moja_Aplikacija.Models;
using System.ComponentModel.DataAnnotations;

namespace Moja_Aplikacija.Attribute
{
    public class RequiredRole : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var role = value as List<RoleModel>;
            if (role == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            if (!role.Any(p => p.Selected))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }

    }
}
