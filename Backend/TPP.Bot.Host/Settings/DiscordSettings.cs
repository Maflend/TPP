namespace TPP.Bot.Host.Settings;
internal class DiscordSettings
{
    public const string Section = "DiscordSettings";

    public ulong ServerId { get; set; }
    public string BotToken { get; set; }
}
