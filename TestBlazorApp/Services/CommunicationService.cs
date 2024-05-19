namespace ItemsBlazorApp.Services
{
    using ItemsBlazorApp.Models;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class CommunicationService : ICommunicationService
    {
        public CommunicationService(
            HttpClient httpClient,
            AppSettings settings
        )
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public async Task<List<Item?>> GetAllAsync()
        {
            var result = await _httpClient.GetAsync($"{_settings.ItemApiAddress}/items");
            if(!result.IsSuccessStatusCode)
            {
                return [];
            }
            var responseContent = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Item?>>(responseContent)!;
        }

        public async Task InsertItemAsync(Item item)
        {
            var jsonContent = JsonSerializer.Serialize(item);
            using var content = new StringContent(jsonContent);
            await _httpClient.PostAsync($"{_settings.ItemApiAddress}/item", content);
            // handle errors
            //if (!result.IsSuccessStatusCode)
            //{
            //    
            //}
        }

        public async Task UpdateItemAsync(Item item)
        {
            var jsonContent = JsonSerializer.Serialize(item);
            using var content = new StringContent(jsonContent);
            await _httpClient.PutAsync($"{_settings.ItemApiAddress}/item", content);
        }

        public async Task DeleteItemAsync(long id)
        {
            await _httpClient.DeleteAsync($"{_settings.ItemApiAddress}/item/{id}");
        }
    }
}
