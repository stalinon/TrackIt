namespace TrackIt.API.Attributes;

/// <summary>
///     Атрибут для отметки, что это контроллер - вебхук телеграма
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
internal class TelegramWebhookAttribute : Attribute;