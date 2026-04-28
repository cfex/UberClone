namespace Location.Infrastructure.Configuration;

public sealed class RedisOptions
{
    public const string SectionName = "Redis";

    public string Host { get; init; } = string.Empty;
    public int Port { get; init; } = 6379;
}