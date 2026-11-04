using Conduit.Application.Abstractions;

namespace Conduit.Application.Features.Authentication.Queries;

public sealed record GetSignupChallengeQuery : IQuery<GetSignupChallengeDto>;
