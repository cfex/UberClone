using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Driver.API.Configuration;

public sealed class DriverGrpcOptions
{
    public const string SectionName = "DriverGrpc";
    [Required] public int Port { get; init; } = 0;
    [Required] public IPAddress IpAddr { get; init; } = IPAddress.None;
}