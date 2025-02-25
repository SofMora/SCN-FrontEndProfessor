using Microsoft.Data.SqlClient;
using SCNProfessor.Models.Domain;
using System.Collections.Generic;
using System.Data;

namespace SCNProfessor.Models.DAO
{
    public class ConsultDAO
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ConsultDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AppointmentConnection");
        }

        public int Insert(Consult consult)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertConsult", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@IdCourse", consult.IdCourse);
                    command.Parameters.AddWithValue("@DescriptionConsult", consult.DescriptionConsult);
                    command.Parameters.AddWithValue("@TypeConsult", consult.TypeConsult);
                    command.Parameters.AddWithValue("@Author", consult.Author);
                    command.Parameters.AddWithValue("@DateAppointment", consult.DateConsult);
                    command.Parameters.AddWithValue("@StatusConsult", consult.StatusConsult);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar la consulta", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public Consult GetById(int id)
        {
            Consult consult = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetConsultById", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            consult = new Consult
                            {
                                Id = (int)reader["Id"],
                                IdCourse = (int)reader["IdCourse"],
                                DescriptionConsult = reader["DescriptionConsult"].ToString(),
                                TypeConsult = reader["TypeConsult"].ToString(),
                                Author = reader["Author"].ToString(),
                                DateConsult = (DateTime)reader["DateAppointment"],
                                StatusConsult = reader["StatusConsult"].ToString()
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener la consulta", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return consult;
        }

        public int Update(Consult consult)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UpdateConsult", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Id", consult.Id);
                    command.Parameters.AddWithValue("@IdCourse", consult.IdCourse);
                    command.Parameters.AddWithValue("@DescriptionConsult", consult.DescriptionConsult);
                    command.Parameters.AddWithValue("@TypeConsult", consult.TypeConsult);
                    command.Parameters.AddWithValue("@Author", consult.Author);
                    command.Parameters.AddWithValue("@DateAppointment", consult.DateConsult);
                    command.Parameters.AddWithValue("@StatusConsult", consult.StatusConsult);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar la consulta", ex);
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
                    SqlCommand command = new SqlCommand("DeleteConsult", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Id", id);

                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar la consulta", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public IEnumerable<Consult> GetAll()
        {
            List<Consult> consults = new List<Consult>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetAllConsults", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Consult consult = new Consult
                            {
                                Id = (int)reader["Id"],
                                IdCourse = (int)reader["IdCourse"],
                                DescriptionConsult = reader["DescriptionConsult"].ToString(),
                                TypeConsult = reader["TypeConsult"].ToString(),
                                Author = reader["Author"].ToString(),
                                DateConsult = (DateTime)reader["DateAppointment"],
                                StatusConsult = reader["StatusConsult"].ToString()
                            };
                            consults.Add(consult);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener las consultas", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return consults;
        }
    }
}