using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Device.Commands.Rename;

public sealed record RenameDeviceCommand : ICommand
{
    public required Guid TargetDeviceId { get; init; }
    public required string Name { get; init; }
}
