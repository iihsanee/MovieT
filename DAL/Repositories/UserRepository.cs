using Microsoft.Data.SqlClient;
using DAL.DTO;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserDTO GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Naam FROM Gebruiker WHERE ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new UserDTO
                    {
                        Id = (int)reader["ID"],
                        Naam = reader["Naam"].ToString()
                    };
                }
            }
            return null;
        }
    }
}