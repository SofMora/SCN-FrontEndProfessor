namespace SCNProfessor.Models.Domain
{
    public class Group
    {

        private int id;
        private Course course;
        private Professor professor;
        private int numberGroup;

        public Group()
        {
        }

        public Group(int id, Course course, Professor professor, int numberGroup)
        {
            this.Id = id;
            this.Course = course;
            this.Professor = professor;
            this.NumberGroup = numberGroup;
        }

        public int Id { get => id; set => id = value; }
        public Course Course { get => course; set => course = value; }
        public Professor Professor { get => professor; set => professor = value; }
        public int NumberGroup { get => numberGroup; set => numberGroup = value; }
    }
}
