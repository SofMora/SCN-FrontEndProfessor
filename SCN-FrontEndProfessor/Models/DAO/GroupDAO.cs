

using Microsoft.Data.SqlClient;
using SCNProfessor.Models.Domain;
using System.Data;

namespace SCNProfessor.Models.DAO
{
   
        public class GroupDAO
        {
            private readonly IConfiguration _configuration;
            string connectionString;

            public GroupDAO(IConfiguration configuration)
            {
                _configuration = configuration;
                connectionString = _configuration.GetConnectionString("ProfessorConnection");
            }

            public int Insert(Group group)
            {
                int result = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("InsertGroup", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdCourse", group.Course.Id);
                        command.Parameters.AddWithValue("@IdProfessor", group.Professor.Id);
                        command.Parameters.AddWithValue("@NumberGroup", group.NumberGroup);

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
