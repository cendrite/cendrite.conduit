using Conduit.API.Common;
using Conduit.API.Requests;
using Conduit.Application.Features.Account.Commands.Recover;
using Conduit.Application.Features.Account.Commands.Signup;
using Conduit.Application.Features.Authentication.Commands.BeginRecovery;
using Conduit.Application.Features.Device.Commands.ApproveLink;
using Conduit.Application.Features.Device.Commands.CreateLinkCode;
using Conduit.Application.Features.Device.Commands.RedeemLinkCode;
using Conduit.Application.Features.Device.Commands.RejectLink;
using Conduit.Application.Features.Device.Queries.GetLinkStatus;
using Conduit.Application.Features.Device.Queries.GetPendingLink;
using Conduit.Application.Features.Authentication.Commands.Logout;
using Conduit.Application.Features.Authentication.Commands.VerifyNonce;
using Conduit.Application.Features.Authentication.Queries;
using Conduit.Application.Features.Device.Commands.Delete;
using Conduit.Application.Features.Device.Queries.GetDevices;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.API.Endpoints;

internal sealed class IdentityEndpoints : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("api/identity")
            .WithTags("identity");

        group.MapPost("logout", async (
                [FromServices] ISender sender,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new LogoutCommand(), ct);
            return result.Success ? Results.Ok() : result.ToProblemDetails();
        })
        .Produces(StatusCodes.Status200OK)
        .Accepts<Guid>("application/json")
        .RequireAuthorization()
        .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPost("verify-nonce", async (
                [FromServices] ISender sender,
                [FromBody] VerifyNonceRequest req,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new VerifyNonceCommand
            {
                UserId = req.UserId,
                NonceId = req.NonceId,
                DeviceId = req.DeviceId,
                Nonce = req.Nonce,
                Announce = req.Announce,
                TokenRequests = req.TokenRequests
            }, ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .Produces<VerifyNonceDto>()
        .Accepts<VerifyNonceRequest>("application/json")
        .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapGet("nonce", async (
                 [FromServices] ISender sender,
                 [AsParameters] GetNonceRequest req,
                 CancellationToken ct) =>
         {
             var result = await sender.Send(new GetNonceQuery() { UserId = req.UserId, DeviceId = req.DeviceId }, ct);
             return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
         })
         .Produces<GetNonceDto>()
         .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapGet("signup-challenge", async (
                [FromServices] ISender sender,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new GetSignupChallengeQuery(), ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .Produces<GetSignupChallengeDto>();

        group.MapPost("recovery-challenge", async (
                [FromServices] ISender sender,
                [FromBody] BeginRecoveryRequest req,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new BeginRecoveryCommand { AccountPublicKey = req.AccountPublicKey }, ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .Produces<BeginRecoveryDto>()
        .Accepts<BeginRecoveryRequest>("application/json")
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status429TooManyRequests);

        var user = group.MapGroup("user");

        user.MapPost("signup", async (
                [FromServices] ISender sender,
                [FromBody] SignupRequest req,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new SignupCommand
            {
                Username = req.Username,
                AccountPublicKey = req.AccountPublicKey,
                ChallengeId = req.ChallengeId,
                PowNonce = req.PowNonce,
                ChallengeSignature = req.ChallengeSignature,
                DevicePublicKey = req.DevicePublicKey,
                DeviceApproval = req.DeviceApproval,
                DeviceAppVersion = req.DeviceAppVersion,
                DeviceType = req.DeviceType,
                DevicePlatform = req.DevicePlatform
            }, ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .Produces<SignupDto>()
        .Accepts<SignupRequest>("application/json")
        .ProducesProblem(StatusCodes.Status400BadRequest);

        user.MapPost("recover", async (
                [FromServices] ISender sender,
                [FromBody] RecoverRequest req,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new RecoverCommand
            {
                AccountPublicKey = req.AccountPublicKey,
                ChallengeId = req.ChallengeId,
                ChallengeSignature = req.ChallengeSignature,
                DevicePublicKey = req.DevicePublicKey,
                DeviceApproval = req.DeviceApproval,
                DeviceAppVersion = req.DeviceAppVersion,
                DeviceType = req.DeviceType,
                DevicePlatform = req.DevicePlatform
            }, ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .Produces<RecoverDto>()
        .Accepts<RecoverRequest>("application/json")
        .ProducesProblem(StatusCodes.Status400BadRequest);

        var link = user.MapGroup("device/link");

        link.MapPost("code", async ([FromServices] ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new CreateLinkCodeCommand(), ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .RequireAuthorization()
        .Produces<CreateLinkCodeDto>()
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status429TooManyRequests);

        link.MapPost("", async ([FromServices] ISender sender, [FromBody] RedeemLinkCodeRequest req, CancellationToken ct) =>
        {
            var result = await sender.Send(new RedeemLinkCodeCommand
            {
                Code = req.Code,
                DevicePublicKey = req.DevicePublicKey,
                DeviceAppVersion = req.DeviceAppVersion,
                DeviceType = req.DeviceType,
                DevicePlatform = req.DevicePlatform
            }, ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .Produces<RedeemLinkCodeDto>()
        .Accepts<RedeemLinkCodeRequest>("application/json")
        .ProducesProblem(StatusCodes.Status400BadRequest);

        link.MapGet("{pendingLinkId:guid}/status", async (
                [FromServices] ISender sender,
                [FromRoute] Guid pendingLinkId,
                [FromHeader(Name = "X-Poll-Secret")] string pollSecret,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new GetLinkStatusQuery { PendingLinkId = pendingLinkId, PollSecret = pollSecret }, ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .Produces<LinkStatusDto>()
        .ProducesProblem(StatusCodes.Status429TooManyRequests);

        link.MapGet("{pendingLinkId:guid}", async ([FromServices] ISender sender, [FromRoute] Guid pendingLinkId, CancellationToken ct) =>
        {
            var result = await sender.Send(new GetPendingLinkQuery { PendingLinkId = pendingLinkId }, ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .RequireAuthorization()
        .Produces<PendingLinkDto>()
        .ProducesProblem(StatusCodes.Status404NotFound);

        link.MapPost("{pendingLinkId:guid}/approve", async (
                [FromServices] ISender sender,
                [FromRoute] Guid pendingLinkId,
                [FromBody] ApproveLinkRequest req,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new ApproveLinkCommand { PendingLinkId = pendingLinkId, Approval = req.Approval }, ct);
            return result.Success ? Results.Ok() : result.ToProblemDetails();
        })
        .RequireAuthorization()
        .Produces(StatusCodes.Status200OK)
        .Accepts<ApproveLinkRequest>("application/json")
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status404NotFound);

        link.MapPost("{pendingLinkId:guid}/reject", async ([FromServices] ISender sender, [FromRoute] Guid pendingLinkId, CancellationToken ct) =>
        {
            var result = await sender.Send(new RejectLinkCommand { PendingLinkId = pendingLinkId }, ct);
            return result.Success ? Results.Ok() : result.ToProblemDetails();
        })
        .RequireAuthorization()
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);

        user.MapGet("devices", async (
                [FromServices] ISender sender,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new GetDevicesQuery(), ct);
            return result.Success ? Results.Ok(result.Value) : result.ToProblemDetails();
        })
        .RequireAuthorization()
        .Produces<GetDevicesDto>()
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status404NotFound);

        user.MapDelete("device/{targetDeviceId:guid}", async (
                [FromServices] ISender sender,
                [FromRoute] Guid targetDeviceId,
                CancellationToken ct) =>
        {
            var result = await sender.Send(new DeleteDeviceCommand { TargetDeviceId = targetDeviceId }, ct);
            return result.Success ? Results.Ok() : result.ToProblemDetails();
        })
        .RequireAuthorization()
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
