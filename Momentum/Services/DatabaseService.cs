﻿using Momentum.Models;
using SQLite;


namespace TaskApp.Services
{
    /* public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskItem>().Wait();
        }

        public Task<List<TaskItem>> GetTasksAsync() => _database.Table<TaskItem>().ToListAsync();
        public Task<int> SaveTaskAsync(TaskItem task) => _database.InsertOrReplaceAsync(task);
        public Task<int> DeleteTaskAsync(TaskItem task) => _database.DeleteAsync(task);
    }*/
}
