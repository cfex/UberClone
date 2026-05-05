using System.ComponentModel.DataAnnotations;

namespace Location.Infrastructure.Configuration;

public sealed class RedisOptions
{
    public const string SectionName = "Redis";

    [Required] public string Host { get; init; } = string.Empty;
    [Required] public int Port { get; init; } = 6379;
}