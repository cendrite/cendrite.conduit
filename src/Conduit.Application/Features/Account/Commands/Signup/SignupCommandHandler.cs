using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.Account.Commands.Signup;

public sealed class SignupCommandHandler(IIdentityUserProvider identityUserProvider) : ICommandHandler<SignupCommand, SignupDto>
{
    public async ValueTask<Result<SignupDto>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        return await identityUserProvider.SignupAsync(request, cancellationToken);
    }
}
