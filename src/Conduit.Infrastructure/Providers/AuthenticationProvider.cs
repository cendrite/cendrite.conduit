using System.Net.Http.Json;
using Conduit.Application.Contracts.Providers;
using Conduit.Application.Features.Authentication.Commands.BeginRecovery;
using Conduit.Application.Features.Authentication.Commands.VerifyNonce;
using Conduit.Application.Features.Authentication.Queries;
using Conduit.Domain.Common;
using Conduit.Infrastructure.Constants;
using Conduit.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace Conduit.Infrastructure.Providers;

internal sealed class AuthenticationProvider(
    IHttpClientFactory clientFactory,
    ILogger<AuthenticationProvider> logger)
    : IIdentityProvider
{
    private readonly HttpClient _client = clientFactory.CreateClient(ClientNames.IdentityClient);

    public async Task<Result> LogoutAsync(CancellationToken ct = default)
    {
        var response = await _client.PostAsync("logout", null, ct);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToFailureResultAsync(ct);
        }

        return Result.Ok();
    }

    public async Task<Result<GetNonceDto>> GetNonceAsync(Guid userId, Guid deviceId, CancellationToken ct = default)
    {
        var response = await _client.GetAsync($"nonce?userId={userId}&deviceId={deviceId}", ct);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToFailureResultAsync<GetNonceDto>(ct);
        }

        return await response.GetValueAsync<GetNonceDto>(ct);

    }

    public async Task<Result<GetSignupChallengeDto>> GetSignupChallengeAsync(CancellationToken ct = default)
    {
        var response = await _client.GetAsync("signup-challenge", ct);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToFailureResultAsync<GetSignupChallengeDto>(ct);
        }

        return await response.GetValueAsync<GetSignupChallengeDto>(ct);
    }

    public async Task<Result<BeginRecoveryDto>> BeginRecoveryAsync(string accountPublicKey, CancellationToken ct = default)
    {
        var response = await _client.PostAsJsonAsync("recovery-challenge", new { AccountPublicKey = accountPublicKey }, ct);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToFailureResultAsync<BeginRecoveryDto>(ct);
        }

        return await response.GetValueAsync<BeginRecoveryDto>(ct);
    }

    public async Task<Result<VerifyNonceDto>> VerifyNonceAsync(Guid userId, Guid nonceId, Guid deviceId, string nonce, bool announce, IReadOnlyList<string> tokenRequests, CancellationToken ct = default)
    {
        var response = await _client.PostAsJsonAsync("verify-nonce", new { UserId = userId, NonceId = nonceId, DeviceId = deviceId, Nonce = nonce, Announce = announce, TokenRequests = tokenRequests }, ct);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("IdentityClient failed with status code {ResponseStatusCode}", response.StatusCode);
            return await response.ToFailureResultAsync<VerifyNonceDto>(ct);
        }

        return await response.GetValueAsync<VerifyNonceDto>(ct);
    }
}
