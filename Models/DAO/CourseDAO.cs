using Microsoft.Data.SqlClient;
using SCNProfessor.Models.Domain;
using System.Data;

namespace SCNProfessor.Models.DAO
{
    public class CourseDAO
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public CourseDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("ProfessorConnection");
        }
        public List<Course> GetAll()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetAllCourses", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    courses.Add(new Course(
                        reader.GetInt32(0),  // Id
                        reader.GetString(1),  // Name
                        reader.GetInt32(2),  // Cycle
                        reader.IsDBNull(3),  // StatusCourse
                        reader.GetString(4),  // Description
                        reader.GetInt32(5)    // IdProfessor
                    ));
                }
                connection.Close();
            }
            return courses;
        }

        public int Update(int id, Course course)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UpdateCourse", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", course.Name);
                    command.Parameters.AddWithValue("@Cycle", course.Cycle);
                    command.Parameters.AddWithValue("@StatusCourse", course.StatusCourse);
                    command.Parameters.AddWithValue("@Description", course.Description);
                    command.Parameters.AddWithValue("@IdProfessor", course.IdProfessor);

                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteCourse", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return result;
        }

        public int Insert(Course course)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertCourse", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", course.Name);
                    command.Parameters.AddWithValue("@Cycle", course.Cycle);
                    command.Parameters.AddWithValue("@StatusCourse", course.StatusCourse);
                    command.Parameters.AddWithValue("@Description", course.Description);
                    command.Parameters.AddWithValue("@IdProfessor", course.IdProfessor);

                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return result;
        }
    }
}
