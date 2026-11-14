using Conduit.Application.Abstractions;
using Conduit.Application.Contracts.Providers;
using Conduit.Domain.Common;

namespace Conduit.Application.Features.UserProfile.Commands.DeleteAvatar;

public sealed class DeleteAvatarCommandHandler(IUserProfileSettingsProvider userProfileSettingsProvider) : ICommandHandler<DeleteAvatarCommand>
{
    public async ValueTask<Result> Handle(DeleteAvatarCommand request, CancellationToken cancellationToken)
        => await userProfileSettingsProvider.DeleteAvatar(cancellationToken);
}
