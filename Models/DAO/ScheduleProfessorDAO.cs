using Microsoft.Data.SqlClient;
using SCNProfessor.Models.Domain;
using System.Collections.Generic;
using System.Data;

namespace SCNProfessor.Models.DAO
{
    public class ScheduleProfessorDAO
    {
    
            private readonly IConfiguration _configuration;
            private readonly string _connectionString;

            public ScheduleProfessorDAO(IConfiguration configuration)
            {
                _configuration = configuration;
                _connectionString = _configuration.GetConnectionString("ProfessorConnection");
            }

            public int Insert(ScheduleProfessor schedule)
            {
                int result = 0;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("InsertSchedule", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("@IdProfessor", schedule.IdProfessor);
                        command.Parameters.AddWithValue("@Day", schedule.Day);
                        command.Parameters.AddWithValue("@Time", schedule.Time);

                        result = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Manejo de excepciones según tus necesidades
                        throw new Exception("Error al insertar el horario", ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return result;
            }

            public ScheduleProfessor GetById(int id)
            {
                ScheduleProfessor schedule = null;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("GetScheduleById", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                schedule = new ScheduleProfessor
                                {
                                    Id = (int)reader["Id"],
                                    IdProfessor = (int)reader["IdProfessor"],
                                    Day = reader["Day"].ToString(),
                                    Time = reader["Time"].ToString()
                                };
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Manejo de excepciones según tus necesidades
                        throw new Exception("Error al obtener el horario", ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return schedule;
            }

            public int Update(ScheduleProfessor schedule)
            {
                int result = 0;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("UpdateSchedule", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("@Id", schedule.Id);
                        command.Parameters.AddWithValue("@IdProfessor", schedule.IdProfessor);
                        command.Parameters.AddWithValue("@Day", schedule.Day);
                        command.Parameters.AddWithValue("@Time", schedule.Time);

                        result = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Manejo de excepciones según tus necesidades
                        throw new Exception("Error al actualizar el horario", ex);
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
                        SqlCommand command = new SqlCommand("DeleteSchedule", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        command.Parameters.AddWithValue("@Id", id);

                        result = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Manejo de excepciones según tus necesidades
                        throw new Exception("Error al eliminar el horario", ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return result;
            }

            public IEnumerable<ScheduleProfessor> GetAll()
            {
                List<ScheduleProfessor> schedules = new List<ScheduleProfessor>();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("GetAllSchedules", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ScheduleProfessor schedule = new ScheduleProfessor
                                {
                                    Id = (int)reader["Id"],
                                    IdProfessor = (int)reader["IdProfessor"],
                                    Day = reader["Day"].ToString(),
                                    Time = reader["Time"].ToString()
                                };
                                schedules.Add(schedule);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Manejo de excepciones según tus necesidades
                        throw new Exception("Error al obtener los horarios", ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return schedules;
            }
        }
    }
