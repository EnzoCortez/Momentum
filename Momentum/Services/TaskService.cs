using System.Net.Http.Json;
using TaskApp.Models;

public class TaskService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "http://TU_IP_LOCAL:5000/api/tasks";

    public TaskService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<TaskItem>> GetTasksAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TaskItem>>(ApiUrl);
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        await _httpClient.PostAsJsonAsync(ApiUrl, task);
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        await _httpClient.PutAsJsonAsync($"{ApiUrl}/{task.Id}", task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
    }
}
