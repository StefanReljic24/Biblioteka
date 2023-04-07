using System.ComponentModel.DataAnnotations;

namespace Moja_Aplikacija.Attribute
{
    public class RequiredIf : ValidationAttribute
    {
        public string PropertyName { get; set; }
        public Object DesiredValue { get; set; }
        
        public RequiredIf(string propertyName,object desiredValue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredValue;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();
            var propertyValue = type.GetProperty(PropertyName)?.GetValue(instance, null);
            if (propertyValue?.ToString() == DesiredValue.ToString())
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
        
    }
}
