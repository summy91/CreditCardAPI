namespace CreditCardAPI.Data.Common
{
    public class ItemOperationResponse<T> : OperationResponse
    {
        public ItemOperationResponse()
            : base()
        {
        }

        public ItemOperationResponse(bool success)
            : base(success)
        {
        }

        public T Item { get; set; }
    }
}
