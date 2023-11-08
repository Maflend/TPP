namespace TPP.MessageBus.Shared.Common;

/// <summary>
/// Статус пользователя в Discord.
/// </summary>
public enum DiscordUserStatus
{
    /// <summary>
    /// Не в сети.
    /// </summary>
    Offline,
    
    /// <summary>
    /// В сети.
    /// </summary>
    Online,
    
    /// <summary>
    /// Бездействует.
    /// </summary>
    Idle,
    
    /// <summary>
    /// Афк.
    /// </summary>
    AFK,

    /// <summary>
    /// Пользователь занят. Просьба не беспокоить.
    /// </summary>
    DoNotDisturb,

    /// <summary>
    /// Невидимый.
    /// </summary>
    Invisible
}
