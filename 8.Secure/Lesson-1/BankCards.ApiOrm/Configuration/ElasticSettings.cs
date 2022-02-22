namespace BankCards.ApiOrm.Configuration;

public class ElasticSettings
{
    public string ConnectionString { get; set; } = null!;
    public string Index { get; set; } = null!;
}
