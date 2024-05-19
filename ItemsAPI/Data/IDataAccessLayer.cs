using ItemsAPI.Models;

public interface IDataAccessLayer
{
    Task DeleteItemAsync(long id);
    Task<Item?> GetItemAsync(long id);
    Task InsertItemAsync(Item person);
    Task<List<Item>> GetAllItemsAsync();
    Task UpdateItemAsync(Item person);
}