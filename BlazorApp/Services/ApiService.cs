using BlazorApp.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class ApiService
    {
        public HttpClient _httpClient;

        public ApiService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<PaginatedList<ToDo>> GetPagedResult(int? pageNumber, string sortField, string sortOrder)
        {
            var response = await _httpClient.GetAsync($"/BlazorDataService/ToDoList/Getv2?pageNumber={pageNumber}&sortField={sortField}&sortOrder={sortOrder}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<PaginatedList<ToDo>>(responseStream, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            });
            return result;
        }

        public async Task<ToDo> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/BlazorDataService/ToDoList/{id}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ToDo>(responseStream);
        }

        public async Task<ToDo> Add(ToDo task)
        {
            var stringData = JsonSerializer.Serialize(task);
            var httpContent = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/BlazorDataService/ToDoList", httpContent);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ToDo>(responseStream);
        }

        public async Task<string> Update(ToDo task)
        {
            var stringData = JsonSerializer.Serialize(task);
            var httpContent = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/BlazorDataService/ToDoList/{task.Id}", httpContent);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
    }
}
