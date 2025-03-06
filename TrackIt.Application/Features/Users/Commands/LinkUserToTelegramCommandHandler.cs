using MediatR;
using TrackIt.Application.Interfaces;

namespace TrackIt.Application.Features.Users.Commands;

/// <summary>
///     Обработчик команды <see cref="LinkUserToTelegramCommand" />
/// </summary>
internal sealed class LinkUserToTelegramCommandHandler(IUserLinkService userLinkService, IUserContext userContext) : IRequestHandler<LinkUserToTelegramCommand>
{
    /// <inheritdoc />
    public async Task Handle(LinkUserToTelegramCommand request, CancellationToken cancellationToken)
    {
        var confirmed = await userLinkService.ConfirmLinkAsync(request.Code, userContext.Email!);
        if (!confirmed)
        {
            throw new UnauthorizedAccessException();
        }
    }
}