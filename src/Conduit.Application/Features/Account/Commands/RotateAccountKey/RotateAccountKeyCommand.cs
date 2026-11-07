using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Account.Commands.RotateAccountKey;

public sealed record RotateAccountKeyCommand : ICommand
{
    public required string NewAccountPublicKey { get; init; }
    public required string Proof { get; init; }
    public required string Approval { get; init; }
}
