using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SCNProfessor.Models.Domain;
using System.Data;

namespace SCNProfessor.Models.DAO
{
    public class ProfessorDAO { 
   private readonly IConfiguration _configuration;
    string connectionString;

    public ProfessorDAO(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("ProfessorConnection");
    }

    public int Insert(Professor professor)
    {
        int result = 0;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("InsertProfessor", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", professor.Name);
                command.Parameters.AddWithValue("@LastName", professor.LastName);
                command.Parameters.AddWithValue("@Email", professor.Email);
                command.Parameters.AddWithValue("@UserName", professor.UserName);
                command.Parameters.AddWithValue("@Password", professor.Password);
                command.Parameters.AddWithValue("@Description", professor.Description);
                command.Parameters.AddWithValue("@Photo", (object)professor.Photo ?? DBNull.Value);
                command.Parameters.AddWithValue("@SocialLink", (object)professor.SocialLink ?? DBNull.Value);
                command.Parameters.AddWithValue("@StatusProfessor", professor.StatusProfessor);

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
                SqlCommand command = new SqlCommand("DeleteProfessor", connection);
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

    public Professor Get(int id)
    {
        Professor professor = null;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("GetProfessorById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                professor = new Professor(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6),
                    reader.IsDBNull(7) ? null : reader.GetString(7),
                    reader.IsDBNull(8) ? null : reader.GetString(8),
                    reader.GetBoolean(9)
                );
            }
            connection.Close();
        }
        return professor;
    }

    public List<Professor> GetAll()
    {
        List<Professor> professors = new List<Professor>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("GetAllProfessors", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                professors.Add(new Professor(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6),
                    reader.IsDBNull(7) ? null : reader.GetString(7),
                    reader.IsDBNull(8) ? null : reader.GetString(8),
                    reader.GetBoolean(9)
                ));
            }
            connection.Close();
        }
        return professors;
        }
    }
}
