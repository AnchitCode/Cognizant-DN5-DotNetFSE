using System;

namespace MVCPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Model
            Student student = new Student("John Doe", 101, "A");

            // Create View
            StudentView view = new StudentView();

            // Create Controller
            StudentController controller = new StudentController(student, view);

            // Display initial details
            controller.UpdateView();

            Console.WriteLine();

            // Update student details
            controller.SetStudentName("Alice Smith");
            controller.SetStudentId(102);
            controller.SetStudentGrade("A+");

            // Display updated details
            controller.UpdateView();
        }
    }
}
