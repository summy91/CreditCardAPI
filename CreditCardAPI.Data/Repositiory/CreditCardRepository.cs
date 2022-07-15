using CreditCardAPI.Data.Common;
using CreditCardAPI.Data.Entities;
using CreditCardAPI.Data.Repositiory.Interfaces;
using MongoDB.Driver;

namespace CreditCardAPI.Data.Repositiory
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly IMongoCollection<CreditCardModel> collection;

        public CreditCardRepository(IDatabaseSettings settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<CreditCardModel>(settings.CollectionName);
        }

        public async Task<OperationResponse> AddCreditCardAsync(CreditCardModel card)
        {
            var response = new OperationResponse();

            try
            {
                if (card == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Credit card model is null";
                    return response;
                }

                await collection.InsertOneAsync(card).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<ListOperationResponse<CreditCardModel>> GetAllCardsAsync()
        {
            var response = new ListOperationResponse<CreditCardModel>();

            try
            {
                response.Items = await collection.FindAsync(a => true).Result.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<ItemOperationResponse<CreditCardModel>> GetCreditCardByIdAsync(string id)
        {
            var response = new ItemOperationResponse<CreditCardModel>();

            try
            {
                response.Item = await collection.FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<OperationResponse> RemoveCreditCardAsync(string id)
        {
            var response = new OperationResponse();

            try
            {
                await collection.DeleteOneAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<OperationResponse> UpdateCreditCardAsync(CreditCardModel card)
        {
            var response = new OperationResponse();

            try
            {
                if (card == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Credit card model is null";
                    return response;
                }

                await collection.FindOneAndReplaceAsync(x => x.Id == card.Id, card);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
