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
                        "SELECT ID, Naam, Wachtwoord, Email FROM Gebruiker WHERE ID = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new UserDTO(
                            (int)reader["ID"],
                            reader["Naam"]?.ToString() ?? string.Empty,
                            reader["Wachtwoord"]?.ToString() ?? string.Empty,
                            reader["Email"]?.ToString() ?? string.Empty
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

        public UserDTO? GetByEmail(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ID, Naam, Wachtwoord, Email FROM Gebruiker WHERE Email = @email", con);
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new UserDTO(
                            (int)reader["ID"],
                            reader["Naam"]?.ToString() ?? string.Empty,
                            reader["Wachtwoord"]?.ToString() ?? string.Empty,
                            reader["Email"]?.ToString() ?? string.Empty
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij ophalen van gebruiker op email.", ex);
            }
            return null;
        }

        public bool EmailExists(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Gebruiker WHERE Email = @email", con);
                    cmd.Parameters.AddWithValue("@email", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij controleren van email.", ex);
            }
        }

        public void AddUser(string naam, string email, string wachtwoord)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(wachtwoord);
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Gebruiker (Naam, Email, Wachtwoord) VALUES (@naam, @email, @wachtwoord)", con);
                    cmd.Parameters.AddWithValue("@naam", naam);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@wachtwoord", hashedWachtwoord);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij aanmaken van gebruiker.", ex);
            }
        }

        public UserDTO? Login(string email, string wachtwoord)
        {
            var user = GetByEmail(email);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(wachtwoord, user.Wachtwoord)) return null;
            return user;
        }

        public void Register(string email, string wachtwoord)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(wachtwoord);
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Gebruiker (Email, Wachtwoord) VALUES (@email, @wachtwoord)", con);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@wachtwoord", hashedWachtwoord);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij registreren van gebruiker.", ex);
            }
        }

        public void UpdateWachtwoord(int gebruikerId, string nieuwWachtwoord)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(nieuwWachtwoord);
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Gebruiker SET Wachtwoord = @wachtwoord WHERE ID = @id", con);
                    cmd.Parameters.AddWithValue("@wachtwoord", hashedWachtwoord);
                    cmd.Parameters.AddWithValue("@id", gebruikerId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij updaten van wachtwoord.", ex);
            }
        }

        public bool VerifyPassword(string wachtwoord, string hashedWachtwoord)
        {
            return BCrypt.Net.BCrypt.Verify(wachtwoord, hashedWachtwoord);
        }
    }
}