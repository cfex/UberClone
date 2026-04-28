using System.ComponentModel.DataAnnotations;

namespace Location.Infrastructure.Configuration;

public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMQ";

    [Required] public string Host { get; init; } = string.Empty;
    [Required] public string VHost { get; init; } = string.Empty;
    [Required] public int Port { get; init; } = 5672;
    [Required] public string Username { get; init; } = string.Empty;
    [Required] public string Password { get; init; } = string.Empty;
}