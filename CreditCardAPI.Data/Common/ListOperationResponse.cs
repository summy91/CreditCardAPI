namespace CreditCardAPI.Data.Common
{
    public class ListOperationResponse<T> : OperationResponse
    {
        public ListOperationResponse()
            : base()
        {
        }

        public ListOperationResponse(bool success)
            : base(success)
        {
        }

        public List<T> Items { get; set; }
    }
}
