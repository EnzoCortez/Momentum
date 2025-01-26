using System.Collections.ObjectModel;
using System.Windows.Input;
using Momentum.Models;
using Momentum.Services;
using Momentum.Views;

namespace Momentum.ViewModels;

public class TaskViewModel : BaseViewModel
{
    private readonly TaskDatabase _database;
    private readonly TaskService _apiService; // ✅ Agregado TaskService

    public ObservableCollection<TaskItem> Tasks { get; set; } = new();
    public TaskItem NewTask { get; set; } = new TaskItem();
    
    private TaskItem _savedTask;
    public TaskItem SavedTask
    {
        get => _savedTask;
        set
        {
            _savedTask = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadTasksCommand { get; }
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }
    public ICommand UpdateTaskCommand { get; }
    public ICommand SaveTaskCommand { get; }

    // Constructor sin parámetros (para XAML)
    public TaskViewModel() { }

    public TaskViewModel(TaskDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _apiService = new TaskService(); // ✅ Inicializar _apiService

        LoadTasksCommand = new Command(async () => await LoadTasks());
        AddTaskCommand = new Command(async () => await OpenAddTaskForm());
        DeleteTaskCommand = new Command<TaskItem>(async (task) => await DeleteTask(task));
        UpdateTaskCommand = new Command<TaskItem>(async (task) => await UpdateTask(task));
        SaveTaskCommand = new Command(async () => await SaveTask());

        LoadTasksCommand.Execute(null);
    }

    private async Task LoadTasks()
    {
        Tasks.Clear();
        var tasks = await _database.GetTasksAsync();
        foreach (var task in tasks)
        {
            Tasks.Add(task);
        }
    }

    private async Task AddTask(TaskItem task)
    {
        if (task == null) return;

        await _database.SaveTaskAsync(task);

        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            await _apiService.AddTaskAsync(task); // ✅ Corregido
        }

        await LoadTasks();
    }

    private async Task UpdateTask(TaskItem task)
    {
        if (task == null) return;
        await _database.SaveTaskAsync(task);
        await LoadTasks();
    }

    private async Task DeleteTask(TaskItem task)
    {
        if (task == null) return;
        await _database.DeleteTaskAsync(task);
        await LoadTasks();
    }

    private async Task OpenAddTaskForm()
    {
        await Shell.Current.GoToAsync(nameof(AddTaskPage));
    }

    private async Task SaveTask()
    {
        if (!string.IsNullOrWhiteSpace(NewTask.Title))
        {
            await _database.SaveTaskAsync(NewTask);

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await _apiService.AddTaskAsync(NewTask);
            }

            SavedTask = NewTask;  // 🔹 Guardar última tarea creada
            NewTask = new TaskItem();  // 🔹 Limpiar formulario
            OnPropertyChanged(nameof(NewTask));
        }
    }

}
