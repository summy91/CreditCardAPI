using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Data.DTO.Request
{
    public class UpdateCreditCardRequest
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        public int TransationAmount { get; set; }
    }
}