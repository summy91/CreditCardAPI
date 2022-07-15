using CreditCardAPI.Data.Common;
using CreditCardAPI.Data.Entities;

namespace CreditCardAPI.Data.Repositiory.Interfaces
{
    public interface ICreditCardRepository
    {
        Task<ListOperationResponse<CreditCardModel>> GetAllCardsAsync();
        Task<ItemOperationResponse<CreditCardModel>> GetCreditCardByIdAsync(string id);
        Task<OperationResponse> AddCreditCardAsync(CreditCardModel card);
        Task<OperationResponse> UpdateCreditCardAsync(CreditCardModel card);
        Task<OperationResponse> RemoveCreditCardAsync(string id);
    }
}
