
namespace DiscordBot.Core.REST
{
    public interface IDotaDiscordBridge
    {
        public Task<int?> GetUserRatingAsync(long accountId);
        public Task<bool> GetUserIsWinAsync(long accountId);
    }
}
