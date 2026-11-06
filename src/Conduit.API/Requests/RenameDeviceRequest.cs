namespace Conduit.API.Requests;

public sealed record RenameDeviceRequest
{
    public required string Name { get; init; }
}
