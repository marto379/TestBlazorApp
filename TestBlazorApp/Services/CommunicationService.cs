using ItemsBlazorApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using System.Text.Json;

namespace ItemsBlazorApp.Services
{
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

        public async Task<List<ItemViewModel?>> GetAllAsync()
        {
            var result = await _httpClient.GetAsync($"{_settings.ItemApiAddress}/items");
            if (!result.IsSuccessStatusCode)
            {
                return [];
            }
            var responseContent = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ItemViewModel?>>(responseContent)!;
        }

        public async Task<ItemViewModel?> GetItemAsync(long id)
        {
            var result = await _httpClient.GetAsync($"{_settings.ItemApiAddress}/item/{id}");
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            var responseContent = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ItemViewModel?>(responseContent)!;
        }

        public async Task InsertItemAsync(ItemViewModel item)
        {
                if (!IsValidItem(item, out var validationErrors))
                {
                    throw new ArgumentException(string.Join(", ", validationErrors));
                }

                var jsonContent = JsonSerializer.Serialize(item);
                using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync($"{_settings.ItemApiAddress}/item", content);
        }

        public async Task<bool> UpdateItemAsync(ItemViewModel item)
        {
            
                if (!IsValidItem(item, out var validationErrors))
                {
                    throw new ArgumentException(string.Join(", ", validationErrors));
                }

                var jsonContent = JsonSerializer.Serialize(item);
                using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                await _httpClient.PutAsync($"{_settings.ItemApiAddress}/item", content);

                return true;
        }

        public async Task DeleteItemAsync(long id)
        {
            await _httpClient.DeleteAsync($"{_settings.ItemApiAddress}/item/{id}");
        }

        private bool IsValidItem(ItemViewModel viewModel, out List<string> validationErrors)
        {
            validationErrors = new List<string>();

            if (viewModel.Price <= 0)
            {
                validationErrors.Add("Price must be a positive number.");
            }

            if (string.IsNullOrEmpty(viewModel.Name))
            {
                validationErrors.Add("Name can't be empty");
            }

            if (viewModel.Name.Length > 100)
            {
                validationErrors.Add("Name can't be longer than 100 chars");
            }

            return !validationErrors.Any();
        }
    }
}
