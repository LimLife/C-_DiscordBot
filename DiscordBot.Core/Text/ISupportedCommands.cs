namespace DiscordBot.Core.Text
{
    public interface ISupportedCommands
    {
        IEnumerable<string> GetSupportedCommands();
    }
}
