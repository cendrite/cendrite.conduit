using Conduit.Application.Features.Account.Commands.Recover;
using Conduit.Application.Features.Account.Commands.RotateAccountKey;
using Conduit.Application.Features.Account.Commands.Signup;
using Conduit.Application.Features.Account.Queries.GetAccountKey;
using Conduit.Application.Features.Device.Commands.ApproveLink;
using Conduit.Application.Features.Device.Commands.CreateLinkCode;
using Conduit.Application.Features.Device.Commands.RedeemLinkCode;
using Conduit.Application.Features.Device.Commands.RejectLink;
using Conduit.Application.Features.Device.Queries.GetLinkStatus;
using Conduit.Application.Features.Device.Queries.GetPendingLink;
using Conduit.Application.Features.Device.Queries.GetDeviceChain;
using Conduit.Application.Features.Device.Queries.GetDevices;
using Conduit.Domain.Common;

namespace Conduit.Application.Contracts.Providers;

public interface IIdentityUserProvider
{
    Task<Result<SignupDto>> SignupAsync(SignupCommand command, CancellationToken ct = default);
    Task<Result<RecoverDto>> RecoverAsync(RecoverCommand command, CancellationToken ct = default);
    Task<Result<GetAccountKeyDto>> GetAccountKeyAsync(CancellationToken ct = default);
    Task<Result> RotateAccountKeyAsync(RotateAccountKeyCommand command, CancellationToken ct = default);
    Task<Result<GetDevicesDto>> GetDevicesAsync(CancellationToken ct = default);
    Task<Result<DeviceChainDto>> GetDeviceChainAsync(Guid connectionId, CancellationToken ct = default);
    Task<Result<CreateLinkCodeDto>> CreateLinkCodeAsync(CancellationToken ct = default);
    Task<Result<RedeemLinkCodeDto>> RedeemLinkCodeAsync(RedeemLinkCodeCommand command, CancellationToken ct = default);
    Task<Result<PendingLinkDto>> GetPendingLinkAsync(Guid pendingLinkId, CancellationToken ct = default);
    Task<Result<LinkStatusDto>> GetLinkStatusAsync(Guid pendingLinkId, string pollSecret, CancellationToken ct = default);
    Task<Result> ApproveLinkAsync(Guid pendingLinkId, string approval, CancellationToken ct = default);
    Task<Result> RejectLinkAsync(Guid pendingLinkId, CancellationToken ct = default);

    Task<Result> DeleteDeviceAsync(Guid targetDeviceId, CancellationToken ct = default);
    Task<Result> RenameDeviceAsync(Guid targetDeviceId, string name, CancellationToken ct = default);
}
