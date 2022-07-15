using System.ComponentModel.DataAnnotations;
using static CreditCardAPI.Data.Validations.CustomValidations;

namespace CreditCardAPI.Data.DTO.Request
{
    public class AddCreditCardRequest
    {
        public string Id { get; set; } = string.Empty;

        [RegularExpression(@"\d{16}", ErrorMessage = "Card number should contain 16 digits length.")]
        [ValidateCreditCardNumber]
        public string CardNumber { get; set; } = string.Empty;

        public int AvailableLimit { get; set; }

        public string CardHolderName { get; set; } = string.Empty;
    }
}