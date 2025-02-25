namespace SCNProfessor.Models.Domain
{
    public class Appointment
    {
        private int id;
        private int idProfessor;
        private int idStudent;
        private int idSchedule;
        private string statusAppointment;
        private string typeAppointment;
        private DateTime dateAppointment;
        private string descriptionAppointment;
        private string subjectAppointment;
        private string commentStatus;

        public Appointment() { 
        
        }

        public Appointment(int id, int idProfessor, int idStudent, int idSchedule, string statusAppointment, string typeAppointment, DateTime dateAppointment, string descriptionAppointment, string subjectAppointment, string commentStatus)
        {
            Id = id;
            IdProfessor = idProfessor;
            IdStudent = idStudent;
            IdSchedule = idSchedule;
            StatusAppointment = statusAppointment;
            TypeAppointment = typeAppointment;
            DateAppointment = dateAppointment;
            DescriptionAppointment = descriptionAppointment;
            SubjectAppointment = subjectAppointment;
            CommentStatus = commentStatus;
        }

        public int Id { get => id; set => id = value; }
        public int IdProfessor { get => idProfessor; set => idProfessor = value; }
        public int IdStudent { get => idStudent; set => idStudent = value; }
        public int IdSchedule { get => idSchedule; set => idSchedule = value; }
        public string StatusAppointment { get => statusAppointment; set => statusAppointment = value; }
        public string TypeAppointment { get => typeAppointment; set => typeAppointment = value; }
        public DateTime DateAppointment { get => dateAppointment; set => dateAppointment = value; }
        public string DescriptionAppointment { get => descriptionAppointment; set => descriptionAppointment = value; }
        public string SubjectAppointment { get => subjectAppointment; set => subjectAppointment = value; }
        public string CommentStatus { get => commentStatus; set => commentStatus = value; }

    }
}
