using Microsoft.Data.SqlClient;
using SCNProfessor.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCNProfessor.Models.DAO
{
    public class NewDAO
    {
        private readonly string _connectionString;

        public NewDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(New news)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertNews", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Title", news.Title);
                    command.Parameters.AddWithValue("@Author", news.Author);
                    command.Parameters.AddWithValue("@TextNews", news.TextNews);
                    command.Parameters.AddWithValue("@DateNews", news.DateNews);
                    command.Parameters.AddWithValue("@Images", news.Images);
                    command.Parameters.AddWithValue("@TypeNews", news.TypeNews);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar la noticia", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public New GetById(int id)
        {
            New news = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetNewsById", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            news = new New
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                TextNews = reader["TextNews"].ToString(),
                                DateNews = (DateTime)reader["DateNews"],
                                Images = reader["Images"].ToString(),
                                TypeNews = reader["TypeNews"].ToString()
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener la noticia", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return news;
        }

        public int Update(New news)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UpdateNews", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Id", news.Id);
                    command.Parameters.AddWithValue("@Title", news.Title);
                    command.Parameters.AddWithValue("@Author", news.Author);
                    command.Parameters.AddWithValue("@TextNews", news.TextNews);
                    command.Parameters.AddWithValue("@DateNews", news.DateNews);
                    command.Parameters.AddWithValue("@Images", news.Images);
                    command.Parameters.AddWithValue("@TypeNews", news.TypeNews);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar la noticia", ex);
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
                    SqlCommand command = new SqlCommand("DeleteNews", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Id", id);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar la noticia", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public IEnumerable<New> GetAll()
        {
            List<New> newsList = new List<New>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetAllNews", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            New news = new New
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                TextNews = reader["TextNews"].ToString(),
                                DateNews = (DateTime)reader["DateNews"],
                                Images = reader["Images"].ToString(),
                                TypeNews = reader["TypeNews"].ToString()
                            };
                            newsList.Add(news);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener las noticias", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return newsList;
        }
    }
}
