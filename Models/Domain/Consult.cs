namespace SCNProfessor.Models.Domain
{
    public class Consult
    {


        private int id;
        private int idCourse;
        private string descriptionConsult;
        private string typeConsult;
        private string author;
        private DateTime dateConsult;
        private string statusConsult;

        public Consult()
        {
        }

        public Consult(int id, int idCourse, string descriptionConsult, string typeConsult, string author, DateTime dateConsult, string statusConsult)
        {
            this.Id = id;
            this.IdCourse = idCourse;
            this.DescriptionConsult = descriptionConsult;
            this.TypeConsult = typeConsult;
            this.Author = author;
            this.DateConsult = dateConsult;
            this.StatusConsult = statusConsult;
        }

        public int Id { get => id; set => id = value; }
        public int IdCourse { get => idCourse; set => idCourse = value; }
        public string DescriptionConsult { get => descriptionConsult; set => descriptionConsult = value; }
        public string TypeConsult { get => typeConsult; set => typeConsult = value; }
        public string Author { get => author; set => author = value; }
        public DateTime DateConsult { get => dateConsult; set => dateConsult = value; }
        public string StatusConsult { get => statusConsult; set => statusConsult = value; }
    }
}
