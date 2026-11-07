using System.Net.Http.Json;
using Conduit.Application.Contracts.Providers;
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

    public async Task<Result<RecoverDto>> RecoverAsync(RecoverCommand command, CancellationToken ct = default)
    {
        var response = await SendAsync(() => _client.PostAsJsonAsync("recover", command, ct), ct);

        if (response is null)
            return Result.Fail<RecoverDto>(Unreachable);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityUserClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToPassthroughFailureResultAsync<RecoverDto>(ct);
        }

        return await response.GetValueAsync<RecoverDto>(ct);
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

    public Task<Result<GetAccountKeyDto>> GetAccountKeyAsync(CancellationToken ct = default) =>
        Relay<GetAccountKeyDto>(() => _client.GetAsync("account-key", ct), ct);

    public Task<Result> RotateAccountKeyAsync(RotateAccountKeyCommand command, CancellationToken ct = default) =>
        Relay(() => _client.PutAsJsonAsync("account-key", command, ct), ct);

    public Task<Result<CreateLinkCodeDto>> CreateLinkCodeAsync(CancellationToken ct = default) =>
        Relay<CreateLinkCodeDto>(() => _client.PostAsync("device/link/code", null, ct), ct);

    public Task<Result<RedeemLinkCodeDto>> RedeemLinkCodeAsync(RedeemLinkCodeCommand command, CancellationToken ct = default) =>
        Relay<RedeemLinkCodeDto>(() => _client.PostAsJsonAsync("device/link", command, ct), ct);

    public Task<Result<PendingLinkDto>> GetPendingLinkAsync(Guid pendingLinkId, CancellationToken ct = default) =>
        Relay<PendingLinkDto>(() => _client.GetAsync($"device/link/{pendingLinkId}", ct), ct);

    public Task<Result<LinkStatusDto>> GetLinkStatusAsync(Guid pendingLinkId, string pollSecret, CancellationToken ct = default) =>
        Relay<LinkStatusDto>(() =>
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"device/link/{pendingLinkId}/status");
            request.Headers.Add("X-Poll-Secret", pollSecret);
            return _client.SendAsync(request, ct);
        }, ct);

    public Task<Result> ApproveLinkAsync(Guid pendingLinkId, string approval, CancellationToken ct = default) =>
        Relay(() => _client.PostAsJsonAsync($"device/link/{pendingLinkId}/approve", new { Approval = approval }, ct), ct);

    public Task<Result> RejectLinkAsync(Guid pendingLinkId, CancellationToken ct = default) =>
        Relay(() => _client.PostAsync($"device/link/{pendingLinkId}/reject", null, ct), ct);

    public Task<Result> RenameDeviceAsync(Guid targetDeviceId, string name, CancellationToken ct = default) =>
        Relay(() => _client.PutAsJsonAsync($"device/{targetDeviceId}/name", new { Name = name }, ct), ct);

    private async Task<Result<T>> Relay<T>(Func<Task<HttpResponseMessage>> send, CancellationToken ct)
    {
        var response = await SendAsync(send, ct);

        if (response is null)
            return Result.Fail<T>(Unreachable);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityUserClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToPassthroughFailureResultAsync<T>(ct);
        }

        return await response.GetValueAsync<T>(ct);
    }

    private async Task<Result> Relay(Func<Task<HttpResponseMessage>> send, CancellationToken ct)
    {
        var response = await SendAsync(send, ct);

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
