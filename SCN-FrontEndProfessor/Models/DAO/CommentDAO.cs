using Microsoft.Data.SqlClient;
using SCNProfessor.Models.Domain;
using System.Data;

namespace SCNProfessor.Models.DAO
{
    public class CommentDAO
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public CommentDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("AdminConnection");
        }

        // Método para obtener todos los comentarios
        public List<Comment> GetAll()
        {
            List<Comment> comments = new List<Comment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetAllComments", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comments.Add(new Comment(
                        reader.GetInt32(0),       // Id
                        reader.GetInt32(1),       // IdNews
                        reader.GetString(2),      //Description
                         reader.GetInt32(3),      // Author
                        reader.GetDateTime(4)     // CommentDate
                    ));
                }
                connection.Close();
            }
            return comments;
        }

        // Método para insertar un nuevo comentario
        public int Insert(Comment comment)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("InsertComment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@IdNews", comment.IdNews);
                command.Parameters.AddWithValue("@Description", comment.Description);
                command.Parameters.AddWithValue("@Author", comment.Author);
                command.Parameters.AddWithValue("@CommentDate", comment.CommentDate);

                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

        // Método para actualizar un comentario existente
        public int Update(int id, Comment comment)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateComment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@IdNews", comment.IdNews);
                command.Parameters.AddWithValue("@Description", comment.Description);
                command.Parameters.AddWithValue("@Author", comment.Author);
                command.Parameters.AddWithValue("@CommentDate", comment.CommentDate);

                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }

        // Método para eliminar un comentario
        public int Delete(int id)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteComment", connection)
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
