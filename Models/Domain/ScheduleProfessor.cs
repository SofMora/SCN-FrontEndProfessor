namespace SCNProfessor.Models.Domain
{
    public class ScheduleProfessor
    {
        private int id;
        private int idProfessor;
        private string day;
        private string time;

       public ScheduleProfessor()
        {
        }

        public ScheduleProfessor(int id, int idProfessor, string day, string time)
        {
            Id = id;
            IdProfessor = idProfessor;
            Day = day;
            Time = time;
        }

        public int Id { get => id; set => id = value; }
        public int IdProfessor { get => idProfessor; set => idProfessor = value; }
        public string Day { get => day; set => day = value; }
        public string Time { get => time; set => time = value; }

    }
}
