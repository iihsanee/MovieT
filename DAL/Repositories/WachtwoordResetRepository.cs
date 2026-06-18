using Microsoft.Data.SqlClient;
using DAL.DTO;
using Interfaces.Interfaces;

namespace DAL.Repositories
{
    public class WachtwoordResetRepository : IWachtwoordResetRepository
    {
        private readonly string _connectionString;

        public WachtwoordResetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SlaResetTokenOp(WachtwoordResetDTO wachtwoordResetDTO)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO WachtwoordReset (Gebruiker_ID, ResetToken, AangemaaktOp, Gebruikt) " +
                        "VALUES (@gebruikerId, @resetToken, @aangemaaktOp, @gebruikt)", con);
                    cmd.Parameters.AddWithValue("@gebruikerId", wachtwoordResetDTO.GebruikerId);
                    cmd.Parameters.AddWithValue("@resetToken", wachtwoordResetDTO.ResetToken);
                    cmd.Parameters.AddWithValue("@aangemaaktOp", wachtwoordResetDTO.AangemaaktOp);
                    cmd.Parameters.AddWithValue("@gebruikt", wachtwoordResetDTO.Gebruikt);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij opslaan van reset token.", ex);
            }
        }

        public WachtwoordResetDTO? GetByToken(string token)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ID, Gebruiker_ID, ResetToken, AangemaaaktOp, Gebruikt " +
                        "FROM WachtwoordReset WHERE ResetToken = @token", con);
                    cmd.Parameters.AddWithValue("@token", token);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new WachtwoordResetDTO(
                            (int)reader["ID"],
                            (int)reader["Gebruiker_ID"],
                            reader["ResetToken"]?.ToString() ?? string.Empty,
                            (DateTime)reader["AangemaaaktOp"],
                            (bool)reader["Gebruikt"]
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij ophalen van reset token.", ex);
            }
            return null;
        }

        public WachtwoordResetDTO? GetByGebruikerId(int gebruikerId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 1 ID, Gebruiker_ID, ResetToken, AangemaaaktOp, Gebruikt " +
                        "FROM WachtwoordReset WHERE Gebruiker_ID = @gebruikerId " +
                        "ORDER BY AangemaaaktOp DESC", con);
                    cmd.Parameters.AddWithValue("@gebruikerId", gebruikerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new WachtwoordResetDTO(
                            (int)reader["ID"],
                            (int)reader["Gebruiker_ID"],
                            reader["ResetToken"]?.ToString() ?? string.Empty,
                            (DateTime)reader["AangemaaaktOp"],
                            (bool)reader["Gebruikt"]
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij ophalen van reset token op gebruiker.", ex);
            }
            return null;
        }

        public void MarkeerAlsGebruikt(string token)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE WachtwoordReset SET Gebruikt = 1 WHERE ResetToken = @token", con);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij markeren van reset token als gebruikt.", ex);
            }
        }

        public void VerwijderOudeTokens(int gebruikerId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM WachtwoordReset WHERE Gebruiker_ID = @gebruikerId", con);
                    cmd.Parameters.AddWithValue("@gebruikerId", gebruikerId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij verwijderen van oude tokens.", ex);
            }
        }

        public void UpdateWachtwoord(int gebruikerId, string nieuwWachtwoord)
        {
            try
            {
                string gehashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(nieuwWachtwoord);
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Gebruiker SET Wachtwoord = @wachtwoord WHERE ID = @gebruikerId", con);
                    cmd.Parameters.AddWithValue("@wachtwoord", gehashedWachtwoord);
                    cmd.Parameters.AddWithValue("@gebruikerId", gebruikerId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij updaten van wachtwoord.", ex);
            }
        }
    }
}