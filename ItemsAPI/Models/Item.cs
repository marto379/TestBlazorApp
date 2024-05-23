namespace ItemsAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Number must be positive. ")]
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
