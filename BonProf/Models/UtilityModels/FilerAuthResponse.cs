using System.Text.Json.Serialization;

namespace BonProf.Models.UtilityModels
{
    public class FilerAuthResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("expiresIn")]
        public long ExpiresIn { get; set; }
    }
}
