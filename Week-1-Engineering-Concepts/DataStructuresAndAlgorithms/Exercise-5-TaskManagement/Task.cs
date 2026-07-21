namespace TaskManagementSystem
{
    public class Task
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public string Status { get; set; }

        public Task(int taskId, string taskName, string status)
        {
            TaskId = taskId;
            TaskName = taskName;
            Status = status;
        }
    }
}
