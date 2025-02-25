using Microsoft.Data.SqlClient;
using SCNProfessor.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCNProfessor.Models.DAO
{
    public class CommentConsultDAO
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CommentConsultDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ProfessorConnection");
        }

        public int Insert(CommentConsult comment)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertCommentConsult", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@IdConsult", comment.IdConsult);
                    command.Parameters.AddWithValue("@DescriptionComment", comment.DescriptionComment);
                    command.Parameters.AddWithValue("@Author", comment.Author);
                    command.Parameters.AddWithValue("@DateComment", comment.DateComment);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar el comentario", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public CommentConsult GetById(int id)
        {
            CommentConsult comment = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetCommentConsultById", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            comment = new CommentConsult
                            {
                                Id = (int)reader["Id"],
                                IdConsult = (int)reader["IdConsult"],
                                DescriptionComment = reader["DescriptionComment"].ToString(),
                                Author = (int)reader["Author"],
                                DateComment = (DateTime)reader["DateComment"]
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el comentario", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return comment;
        }

        public int Update(CommentConsult comment)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UpdateCommentConsult", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Id", comment.Id);
                    command.Parameters.AddWithValue("@IdConsult", comment.IdConsult);
                    command.Parameters.AddWithValue("@DescriptionComment", comment.DescriptionComment);
                    command.Parameters.AddWithValue("@Author", comment.Author);
                    command.Parameters.AddWithValue("@DateComment", comment.DateComment);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar el comentario", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteCommentConsult", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Id", id);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar el comentario", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public IEnumerable<CommentConsult> GetAll()
        {
            List<CommentConsult> comments = new List<CommentConsult>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetAllCommentConsults", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CommentConsult comment = new CommentConsult
                            {
                                Id = (int)reader["Id"],
                                IdConsult = (int)reader["IdConsult"],
                                DescriptionComment = reader["DescriptionComment"].ToString(),
                                Author = (int)reader["Author"],
                                DateComment = (DateTime)reader["DateComment"]
                            };
                            comments.Add(comment);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener los comentarios", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return comments;
        }
    }
}
