using System.Text.Json.Serialization;

namespace ClientForWeb.Models
{
    public class CategoryViewModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
