using System.Collections.ObjectModel;
using System.Windows.Input;
using Momentum.Models;
using SQLite;




public class TaskViewModel : BaseViewModel
{
    private readonly TaskService _taskService;
    public ObservableCollection<TaskItem> Tasks { get; set; } = new();

    public ICommand LoadTasksCommand { get; }
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand UpdateTaskCommand { get; }

    public TaskViewModel()
    {
        _taskService = new TaskService();
        LoadTasksCommand = new Command(async () => await LoadTasks());
        AddTaskCommand = new Command<TaskItem>(async (task) => await AddTask(task));
        DeleteTaskCommand = new Command<int>(async (id) => await DeleteTask(id));
        UpdateTaskCommand = new Command<TaskItem>(async (task) => await UpdateTask(task));
    }

    private async Task LoadTasks()
    {
        Tasks.Clear();
        var tasks = await _taskService.GetTasksAsync();
        foreach (var task in tasks)
        {
            Tasks.Add(task);
        }
    }

    private async Task AddTask(TaskItem task)
    {
        await _taskService.AddTaskAsync(task);
        await LoadTasks();
    }

    private async Task UpdateTask(TaskItem task)
    {
        await _taskService.UpdateTaskAsync(task);
        await LoadTasks();
    }

    private async Task DeleteTask(int id)
    {
        await _taskService.DeleteTaskAsync(id);
        await LoadTasks();
    }
}
