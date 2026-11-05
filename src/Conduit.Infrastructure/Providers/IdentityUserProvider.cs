using System.Net.Http.Json;
using Conduit.Application.Contracts.Providers;
using Conduit.Application.Features.Account.Commands.Signup;
using Conduit.Application.Features.Device.Queries.GetDevices;
using Conduit.Domain.Common;
using Conduit.Infrastructure.Constants;
using Conduit.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace Conduit.Infrastructure.Providers;

internal sealed class IdentityUserProvider(
    IHttpClientFactory clientFactory,
    ILogger<IdentityUserProvider> logger)
    : IIdentityUserProvider
{
    private static readonly Error Unreachable = Error.Failure("An unexpected error occurred.");

    private readonly HttpClient _client = clientFactory.CreateClient(ClientNames.IdentityUserClient);

    public async Task<Result<SignupDto>> SignupAsync(SignupCommand command, CancellationToken ct = default)
    {
        var response = await SendAsync(() => _client.PostAsJsonAsync("signup", command, ct), ct);

        if (response is null)
            return Result.Fail<SignupDto>(Unreachable);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityUserClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToPassthroughFailureResultAsync<SignupDto>(ct);
        }

        return await response.GetValueAsync<SignupDto>(ct);
    }

    public async Task<Result<GetDevicesDto>> GetDevicesAsync(CancellationToken ct = default)
    {
        var response = await SendAsync(() => _client.GetAsync("devices", ct), ct);

        if (response is null)
            return Result.Fail<GetDevicesDto>(Unreachable);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityUserClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToPassthroughFailureResultAsync<GetDevicesDto>(ct);
        }

        return await response.GetValueAsync<GetDevicesDto>(ct);
    }

    public async Task<Result> DeleteDeviceAsync(Guid targetDeviceId, CancellationToken ct = default)
    {
        var response = await SendAsync(() => _client.DeleteAsync($"device/{targetDeviceId}", ct), ct);

        if (response is null)
            return Result.Fail(Unreachable);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityUserClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToPassthroughFailureResultAsync(ct);
        }

        return Result.Ok();
    }

    private async Task<HttpResponseMessage?> SendAsync(Func<Task<HttpResponseMessage>> send, CancellationToken ct)
    {
        try
        {
            return await send();
        }
        catch (Exception ex) when (ex is HttpRequestException || (ex is TaskCanceledException && !ct.IsCancellationRequested))
        {
            logger.LogError(ex, "IdentityUserClient could not reach Identity");
            return null;
        }
    }
}
