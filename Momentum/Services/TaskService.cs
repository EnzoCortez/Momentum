using Momentum.Models;
using System.Net.Http.Json;

public class TaskService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string ApiUrl = "http://localhost:5122/api/Task";

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TaskItem>>() ?? new List<TaskItem>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener tareas: {ex.Message}");
            return new List<TaskItem>(); // Retorna lista vacía en caso de error
        }
    }

    public async Task<bool> AddTaskAsync(TaskItem task)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(ApiUrl, task);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar tarea: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateTaskAsync(TaskItem task)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{task.Id}", task);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar tarea: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar tarea: {ex.Message}");
            return false;
        }
    }
}
