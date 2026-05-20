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
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ID, Naam, Wachtwoord FROM Gebruiker WHERE ID = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new UserDTO(
                            (int)reader["ID"],
                            reader["Naam"]?.ToString() ?? string.Empty,
                            reader["Wachtwoord"]?.ToString() ?? string.Empty
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van gebruiker met ID {id}.", ex);
            }
            return null;
        }

        public bool UsernameExists(string naam)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Gebruiker WHERE Naam = @naam", con);
                    cmd.Parameters.AddWithValue("@naam", naam);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij controleren van gebruikersnaam.", ex);
            }
        }

        public void AddUser(string naam, string wachtwoord)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Gebruiker (Naam, Wachtwoord) VALUES (@naam, @wachtwoord)", con);
                    cmd.Parameters.AddWithValue("@naam", naam);
                    cmd.Parameters.AddWithValue("@wachtwoord", wachtwoord);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij aanmaken van gebruiker.", ex);
            }
        }
    }
}