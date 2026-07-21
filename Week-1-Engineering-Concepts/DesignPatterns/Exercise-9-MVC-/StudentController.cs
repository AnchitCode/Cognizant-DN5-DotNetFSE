namespace MVCPatternExample
{
    public class StudentController
    {
        private Student model;
        private StudentView view;

        public StudentController(Student model, StudentView view)
        {
            this.model = model;
            this.view = view;
        }

        public void SetStudentName(string name)
        {
            model.Name = name;
        }

        public string GetStudentName()
        {
            return model.Name;
        }

        public void SetStudentId(int id)
        {
            model.Id = id;
        }

        public int GetStudentId()
        {
            return model.Id;
        }

        public void SetStudentGrade(string grade)
        {
            model.Grade = grade;
        }

        public string GetStudentGrade()
        {
            return model.Grade;
        }

        public void UpdateView()
        {
            view.DisplayStudentDetails(model.Name, model.Id, model.Grade);
        }
    }
}
