using Microsoft.Data.SqlClient;
using DAL.DTO;
using Interfaces.Interfaces;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserDTO? GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Naam FROM Gebruiker WHERE ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new UserDTO(
                        (int)reader["ID"],
                        reader["Naam"]?.ToString() ?? string.Empty
                    );
                }
            }
            return null;
        }
    }
}