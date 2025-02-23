namespace SCNProfessor.Models.Domain
{
    public class Course
    {
        private int id;
        private string name;
        private int cycle;
        private bool statusCourse;
        private string description;
        private int idProfessor;

        public Course()
        {
        }

        public Course(int id, string name, int cycle, bool statusCourse, string description, int idProfessor)
        {
            this.Id = id;
            this.Name = name;
            this.Cycle = cycle;
            this.StatusCourse = statusCourse;
            this.Description = description;
            this.IdProfessor = idProfessor;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Cycle { get => cycle; set => cycle = value; }
        public bool StatusCourse { get => statusCourse; set => statusCourse = value; }
        public string Description { get => description; set => description = value; }
        public int IdProfessor { get => idProfessor; set => idProfessor = value; }
    }
}
