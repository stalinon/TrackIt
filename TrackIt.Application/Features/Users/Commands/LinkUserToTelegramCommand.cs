using System.Text.Json.Serialization;
using MediatR;

namespace TrackIt.Application.Features.Users.Commands;

/// <summary>
///     Прикрепить пользователя к Телеграм
/// </summary>
public sealed class LinkUserToTelegramCommand : IRequest
{
    /// <summary>
    ///     Код для привязки
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;
}