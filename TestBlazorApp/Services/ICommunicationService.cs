namespace ItemsBlazorApp.Services
{
    using ItemsBlazorApp.Models;

    public interface ICommunicationService
    {
        Task<List<ItemViewModel?>> GetAllAsync();

        Task InsertItemAsync(ItemViewModel item);

        Task UpdateItemAsync(ItemViewModel item);

        Task DeleteItemAsync(long id);

        Task<ItemViewModel?> GetItemAsync(long id);
    }
}
