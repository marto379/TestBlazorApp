namespace ItemsBlazorApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class ItemViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Number must be positive")]
        public decimal Price { get; set; }

        [JsonPropertyName("dateAdded")]
        [Required(ErrorMessage = "Date is required")]
        [Range(typeof(DateTime), "1/1/2000", "1/1/2025", ErrorMessage = "Date must be between {1:d} and {2:d}")]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
