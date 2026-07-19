namespace TaskManagementSystem
{
    public class TaskNode
    {
        public Task Data { get; set; }

        public TaskNode Next { get; set; }

        public TaskNode(Task task)
        {
            Data = task;
            Next = null;
        }
    }
}
