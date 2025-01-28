using Momentum.Models;
using Newtonsoft.Json;  // ✅ Agregado para JsonConvert
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

public class TaskService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string ApiUrl = "http://192.168.100.117:5122/api/Task";  // Cambia localhost por tu IP local
                                                                       

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
            return new List<TaskItem>();  // ✅ Retorna lista vacía en caso de error
        }
    }

    public async Task<bool> AddTaskAsync(TaskItem task)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(ApiUrl, task);
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
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar tarea: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> AddTaskToServer(TaskItem task)
    {
        try
        {
            var json = JsonConvert.SerializeObject(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);  // ✅ Corregido
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al sincronizar con API: {ex.Message}");
            return false;
        }
    }
}
