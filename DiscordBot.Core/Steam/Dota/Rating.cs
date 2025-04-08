
using System.Text.Json.Serialization;

namespace DiscordBot.Core.Steam.Dota
{
    public class Rating
    {
        [JsonPropertyName("solo_competitive_rank")]
        public int? SoloCompetitiveRank { get; set; }

        [JsonPropertyName("competitive_rank")]
        public int? CompetitiveRank { get; set; }

    }
}
