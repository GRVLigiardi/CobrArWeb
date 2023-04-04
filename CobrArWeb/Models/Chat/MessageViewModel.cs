using Newtonsoft.Json;

namespace CobrArWeb.Models.Chat
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
