using Microsoft.Data.SqlClient;
using SCNProfessor.Models.Domain;
using System.Data;

namespace SCNProfessor.Models.DAO
{
    public class AppointmentDAO
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public AppointmentDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("AppointmentConnection");
        }

        // Método para obtener todas las citas
        public List<Appointment> GetAll()
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetAllAppointments", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    appointments.Add(new Appointment(
                        reader.GetInt32(0),      // Id
                        reader.GetInt32(1),      // IdProfessor
                        reader.GetInt32(2),      // IdStudent
                        reader.GetInt32(3),      // IdSchedule
                        reader.GetString(4),     // StatusAppointment
                        reader.GetString(5),     // TypeAppointment
                        reader.GetDateTime(6),   // DateAppointment
                        reader.GetString(7),     // DescriptionAppointment
                        reader.GetString(8),     // SubjectAppointment
                        reader.GetString(9)      // CommentStatus
                    ));
                }
                connection.Close();
            }
            return appointments;
        }

        // Método para insertar una nueva cita
        public int Insert(Appointment appointment)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("InsertAppointment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdProfessor", appointment.IdProfessor);
                command.Parameters.AddWithValue("@IdStudent", appointment.IdStudent);
                command.Parameters.AddWithValue("@IdSchedule", appointment.IdSchedule);
                command.Parameters.AddWithValue("@StatusAppointment", appointment.StatusAppointment);
                command.Parameters.AddWithValue("@TypeAppointment", appointment.TypeAppointment);
                command.Parameters.AddWithValue("@DateAppointment", appointment.DateAppointment);
                command.Parameters.AddWithValue("@DescriptionAppointment", appointment.DescriptionAppointment);
                command.Parameters.AddWithValue("@SubjectAppointment", appointment.SubjectAppointment);
                command.Parameters.AddWithValue("@CommentStatus", appointment.CommentStatus);

                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

        // Método para actualizar una cita
        public int Update(int id, Appointment appointment)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateAppointment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@IdProfessor", appointment.IdProfessor);
                command.Parameters.AddWithValue("@IdStudent", appointment.IdStudent);
                command.Parameters.AddWithValue("@IdSchedule", appointment.IdSchedule);
                command.Parameters.AddWithValue("@StatusAppointment", appointment.StatusAppointment);
                command.Parameters.AddWithValue("@TypeAppointment", appointment.TypeAppointment);
                command.Parameters.AddWithValue("@DateAppointment", appointment.DateAppointment);
                command.Parameters.AddWithValue("@DescriptionAppointment", appointment.DescriptionAppointment);
                command.Parameters.AddWithValue("@SubjectAppointment", appointment.SubjectAppointment);
                command.Parameters.AddWithValue("@CommentStatus", appointment.CommentStatus);

                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

        // Método para eliminar una cita
        public int Delete(int id)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteAppointment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);

                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }
    }
}
