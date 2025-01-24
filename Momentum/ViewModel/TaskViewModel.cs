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

        public TaskViewModel(TaskDatabase database)
        {
            _database = database;

            LoadTasksCommand = new Command(async () => await LoadTasks());
            AddTaskCommand = new Command<TaskItem>(async (task) => await AddTask(task));
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
            await _database.SaveTaskAsync(task);
            await LoadTasks();
        }

        private async Task UpdateTask(TaskItem task)
        {
            await _database.SaveTaskAsync(task);
            await LoadTasks();
        }

        private async Task DeleteTask(TaskItem task)
        {
            await _database.DeleteTaskAsync(task);
            await LoadTasks();
        }
    }
}
