namespace ItemsBlazorApp.Models
{
    using System.Text.Json.Serialization;

    public class Item
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("dateAdded")]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
