namespace CreditCardAPI.Data
{
    public interface IDatabaseSettings
    {
        string CollectionName { get; set; }
        string Connectionstring { get; set; }
        string DatabaseName { get; set; }
    }
}
