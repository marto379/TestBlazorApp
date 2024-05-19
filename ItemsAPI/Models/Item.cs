namespace ItemsAPI.Models
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
