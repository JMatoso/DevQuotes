namespace DevQuotes.Infrastructure.Options;

public class CorsOptions
{
    public const string Cors = "CorsOptions";
    public string PolicyName { get; set; } = "";
    public string[] AllowedOrigins { get; set; } = [];
}
