using System.Collections.ObjectModel;
using System.Windows.Input;
using Momentum.Models;
using Momentum.Services;

namespace Momentum.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private readonly TaskDatabase _database;
        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        public ICommand LoadTasksCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }

        //  Constructor sin parámetros (necesario para XAML)
        public TaskViewModel() { }

        public TaskViewModel(TaskDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));

            LoadTasksCommand = new Command(async () => await LoadTasks());
            AddTaskCommand = new Command(async () => await OpenAddTaskForm());
            DeleteTaskCommand = new Command<TaskItem>(async (task) => await DeleteTask(task));
            UpdateTaskCommand = new Command<TaskItem>(async (task) => await UpdateTask(task));

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

            var tasks = await _database.GetTasksAsync();
            Console.WriteLine($"Tareas guardadas: {tasks.Count}");

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

    }
}
