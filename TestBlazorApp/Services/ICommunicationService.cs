namespace ItemsBlazorApp.Services
{
    using ItemsBlazorApp.Models;

    public interface ICommunicationService
    {
        Task<List<Item?>> GetAllAsync();

        Task InsertItemAsync(Item item);

        Task UpdateItemAsync(Item item);

        Task DeleteItemAsync(long id);
    }
}
