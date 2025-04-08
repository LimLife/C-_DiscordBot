using System.Text.Json.Serialization;
using DiscordBot.Core.Steam.Dota;
using DiscordBot.Core.REST;
using System.Text.Json;


namespace DiscordBot.NetworkKit.SteamCommand
{
    public class DotaDiscordBridge: IDotaDiscordBridge
    {
        private readonly HttpClient _httpClient;
        public DotaDiscordBridge(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> GetUserIsWinAsync(long accountId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"players/{accountId}/matches?limit=1");

                if (!response.IsSuccessStatusCode) return false;


                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var matches = JsonSerializer.Deserialize<MatchHistory[]>(content, options);

                if (matches == null || matches.Length == 0) return false;

                var lastMatch = matches[0];
                bool isRadiant = lastMatch.Player_Slot <= 127;
                bool radiantWin = lastMatch.Radiant_Win;

                return isRadiant == radiantWin;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int?> GetUserRatingAsync(long accountId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"players/{accountId}/ratings");

                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };
                var ratings = await JsonSerializer.DeserializeAsync<List<Rating>>(await response.Content.ReadAsStreamAsync(), options);
                var latestRating = ratings?.FirstOrDefault();
                return latestRating?.SoloCompetitiveRank;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
