using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Momentum.Models;
using Momentum.Services;
using Momentum.Views;

namespace Momentum.ViewModels
{
    public partial class TaskViewModel : ObservableObject
    {
        private readonly TaskDatabase _database;
        private readonly TaskService _apiService;

        [ObservableProperty]
        private ObservableCollection<TaskItem> tasks = new();

        [ObservableProperty]
        private TaskItem newTask = new TaskItem();

        [ObservableProperty]
        private TaskItem savedTask;

        public ICommand LoadTasksCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }
        public ICommand SaveTaskCommand { get; }

        // 🔹 Constructor sin parámetros necesario para XAML
        public TaskViewModel() : this(App.Database, App.ApiService) { }

        // 🔹 Constructor principal con inyección de dependencias
        public TaskViewModel(TaskDatabase database, TaskService apiService)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));

            LoadTasksCommand = new AsyncRelayCommand(LoadTasks);
            AddTaskCommand = new AsyncRelayCommand<TaskItem>(AddTask);
            DeleteTaskCommand = new AsyncRelayCommand<TaskItem>(DeleteTask);
            UpdateTaskCommand = new AsyncRelayCommand<TaskItem>(UpdateTask);
            SaveTaskCommand = new AsyncRelayCommand(SaveTask);

            LoadTasksCommand.Execute(null);
        }

        private async Task LoadTasks()
        {
            Tasks.Clear();
            var tasksList = await _database.GetTasksAsync();
            foreach (var task in tasksList)
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
                await _apiService.AddTaskAsync(task);
            }

            Tasks.Add(task); // ✅ Se añade la tarea a la lista para actualizar la UI
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
            Tasks.Remove(task); // ✅ Se elimina la tarea de la UI
        }

        private async Task SaveTask()
        {
            if (string.IsNullOrWhiteSpace(NewTask.Title))
            {
                return;
            }

            await _database.SaveTaskAsync(NewTask);

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await _apiService.AddTaskAsync(NewTask);
            }

            Tasks.Add(NewTask); // ✅ Se actualiza la lista de tareas
            SavedTask = NewTask;
            NewTask = new TaskItem();
        }
    }
}
