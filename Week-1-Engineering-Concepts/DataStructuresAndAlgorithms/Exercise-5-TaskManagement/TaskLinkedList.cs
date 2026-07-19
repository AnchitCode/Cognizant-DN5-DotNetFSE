using System;

namespace TaskManagementSystem
{
    public class TaskLinkedList
    {
        private TaskNode head;

        // Add Task
        public void AddTask(Task task)
        {
            TaskNode newNode = new TaskNode(task);

            if (head == null)
            {
                head = newNode;
                return;
            }

            TaskNode current = head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newNode;
        }

        // Search Task
        public Task SearchTask(int taskId)
        {
            TaskNode current = head;

            while (current != null)
            {
                if (current.Data.TaskId == taskId)
                {
                    return current.Data;
                }

                current = current.Next;
            }

            return null;
        }

        // Traverse Tasks
        public void TraverseTasks()
        {
            TaskNode current = head;

            Console.WriteLine("\nTask List:");

            while (current != null)
            {
                Console.WriteLine(
                    $"ID: {current.Data.TaskId}, " +
                    $"Task: {current.Data.TaskName}, " +
                    $"Status: {current.Data.Status}");

                current = current.Next;
            }
        }

        // Delete Task
        public void DeleteTask(int taskId)
        {
            if (head == null)
            {
                Console.WriteLine("Task list is empty.");
                return;
            }

            if (head.Data.TaskId == taskId)
            {
                head = head.Next;
                Console.WriteLine("Task deleted successfully.");
                return;
            }

            TaskNode current = head;

            while (current.Next != null &&
                   current.Next.Data.TaskId != taskId)
            {
                current = current.Next;
            }

            if (current.Next == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            current.Next = current.Next.Next;

            Console.WriteLine("Task deleted successfully.");
        }
    }
}
