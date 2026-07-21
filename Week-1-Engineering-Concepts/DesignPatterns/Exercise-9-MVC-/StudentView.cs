using System;

namespace MVCPatternExample
{
    public class StudentView
    {
        public void DisplayStudentDetails(string studentName, int studentId, string studentGrade)
        {
            Console.WriteLine("Student Details:");
            Console.WriteLine($"Name  : {studentName}");
            Console.WriteLine($"ID    : {studentId}");
            Console.WriteLine($"Grade : {studentGrade}");
        }
    }
}
