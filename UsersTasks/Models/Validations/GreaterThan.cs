using System.ComponentModel.DataAnnotations;

namespace UsersTasks.Models.Validations
{
   
    public class GreaterThanZero : ValidationAttribute
    {
        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext)
        {

            if (value == null) return ValidationResult.Success;

            int valueInt = Convert.ToInt32(value);
            Console.WriteLine("Value : " + valueInt);
            if (valueInt > 0)
            {
                return ValidationResult.Success;
            }
            else
            {
  
                return new ValidationResult
                    (validationContext.DisplayName + " should be greater than zero.");
            }
        }
    }
}
