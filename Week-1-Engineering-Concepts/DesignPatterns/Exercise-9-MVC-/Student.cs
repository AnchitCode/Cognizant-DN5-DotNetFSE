namespace MVCPatternExample
{
    public class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Grade { get; set; }

        public Student(string name, int id, string grade)
        {
            Name = name;
            Id = id;
            Grade = grade;
        }
    }
}
