using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Data.Validations
{
    public class CustomValidations
    {
        public sealed class ValidateCreditCardNumber : ValidationAttribute
        {
            private string errorMessage = "Please enter a valid credit card number.";

            protected override ValidationResult IsValid(object input, ValidationContext validationContext)
            {
                string creditCardNumber = input?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(creditCardNumber) || creditCardNumber.Length < 16)
                {
                    return new ValidationResult(errorMessage);
                }

                int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                                .Reverse()
                                .Select((e, i) => (e - 48) * (i % 2 == 0 ? 1 : 2))
                                .Sum((e) => e / 10 + e % 10);

                if (sumOfDigits % 10 == 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(errorMessage);
                }
            }
        }
    }
}
