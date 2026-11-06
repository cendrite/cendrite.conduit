using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Device.Commands.CreateLinkCode;

public sealed record CreateLinkCodeCommand : ICommand<CreateLinkCodeDto>;
