using System.ComponentModel.DataAnnotations;

namespace Driver.Infrastructure.Configuration;

public sealed class DriverGrpcOptions
{
    public const string SectionName = "DriverGrpc";
    [Required] public int Port { get; init; } = 0;
    [Required] public string IpAddr { get; init; } = "";
}