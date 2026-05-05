using System.ComponentModel.DataAnnotations;

namespace Location.Infrastructure.Configuration;

public sealed class LocationGrpcOptions
{
    public const string SectionName = "LocationGrpc";
    [Required] public int Port { get; init; } = 0;
    [Required] public string IpAddr { get; init; } = string.Empty;
}