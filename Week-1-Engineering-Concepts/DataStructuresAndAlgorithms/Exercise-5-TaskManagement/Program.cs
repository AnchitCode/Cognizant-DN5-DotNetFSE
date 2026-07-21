using System;

namespace TaskManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskLinkedList taskList = new TaskLinkedList();

            // Add Tasks
            taskList.AddTask(new Task(1, "Complete DSA Assignment", "Pending"));
            taskList.AddTask(new Task(2, "Submit Project", "Completed"));
            taskList.AddTask(new Task(3, "Prepare for Interview", "In Progress"));

            // Traverse Tasks
            taskList.TraverseTasks();

            // Search Task
            Console.WriteLine("\nSearching for Task ID 2...");

            Task task = taskList.SearchTask(2);

            if (task != null)
            {
                Console.WriteLine(
                    $"Found: {task.TaskName}, Status: {task.Status}");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }

            // Delete Task
            Console.WriteLine("\nDeleting Task ID 2...");
            taskList.DeleteTask(2);

            // Traverse Again
            taskList.TraverseTasks();
        }
    }
}
