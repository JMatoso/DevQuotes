namespace DevQuotes.Infrastructure.Options;

public sealed class CorsOptions
{
    public const string Cors = "CorsOptions";
    public string PolicyName { get; set; } = string.Empty;
    public string[] AllowedOrigins { get; set; } = [];
}
