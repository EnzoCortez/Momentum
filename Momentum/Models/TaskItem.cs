using SQLite;

namespace Momentum.Models
{
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string Category { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
    }
}
