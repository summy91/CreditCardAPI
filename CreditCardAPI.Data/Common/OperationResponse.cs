namespace CreditCardAPI.Data.Common
{
    public class OperationResponse
    {
        public OperationResponse(bool success)
        {
            Success = success;
        }

        public OperationResponse()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string FriendlyMessage { get; set; }
    }
}
