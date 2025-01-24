using SQLite;
using Momentum.Models;

namespace Momentum.Services
{
    public class TaskDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public TaskDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskItem>().Wait();
        }

        public Task<List<TaskItem>> GetTasksAsync()
        {
            return _database.Table<TaskItem>().ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskItem task)
        {
            if (task.Id == 0)
                return _database.InsertAsync(task);
            else
                return _database.UpdateAsync(task);
        }

        public Task<int> DeleteTaskAsync(TaskItem task)
        {
            return _database.DeleteAsync(task);
        }
    }
}
