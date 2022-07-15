namespace CreditCardAPI.Data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CollectionName { get; set; } = string.Empty;
        public string Connectionstring { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
