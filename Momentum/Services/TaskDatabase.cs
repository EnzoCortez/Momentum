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

        public async Task SaveTaskAsync(TaskItem task)
        {
            await _database.InsertOrReplaceAsync(task);
        }


        public Task<int> DeleteTaskAsync(TaskItem task)
        {
            return _database.DeleteAsync(task);
        }
    }
}
