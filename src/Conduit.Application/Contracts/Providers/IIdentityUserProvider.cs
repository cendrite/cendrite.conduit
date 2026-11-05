using Conduit.Application.Features.Account.Commands.Recover;
using Conduit.Application.Features.Account.Commands.Signup;
using Conduit.Application.Features.Device.Queries.GetDevices;
using Conduit.Domain.Common;

namespace Conduit.Application.Contracts.Providers;

public interface IIdentityUserProvider
{
    Task<Result<SignupDto>> SignupAsync(SignupCommand command, CancellationToken ct = default);
    Task<Result<RecoverDto>> RecoverAsync(RecoverCommand command, CancellationToken ct = default);
    Task<Result<GetDevicesDto>> GetDevicesAsync(CancellationToken ct = default);

    Task<Result> DeleteDeviceAsync(Guid targetDeviceId, CancellationToken ct = default);
}
