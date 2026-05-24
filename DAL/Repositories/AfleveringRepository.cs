using Microsoft.Data.SqlClient;
using DAL.DTO;
using Interfaces.Interfaces;

namespace DAL.Repositories
{
    public class AfleveringRepository : IAfleveringRepository
    {
        private readonly string _connectionString;

        public AfleveringRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<AfleveringDTO> GetBySeizoenId(int seizoenId)
        {
            var items = new List<AfleveringDTO>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ID, SeizoenID, Titel, Afleveringsnummer, Duurtijd FROM Aflevering WHERE SeizoenID = @seizoenId ORDER BY Afleveringsnummer", con);
                    cmd.Parameters.AddWithValue("@seizoenId", seizoenId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        items.Add(new AfleveringDTO(
                            (int)reader["ID"],
                            (int)reader["SeizoenID"],
                            reader["Titel"]?.ToString() ?? string.Empty,
                            (int)reader["Afleveringsnummer"],
                            (int)reader["Duurtijd"]
                        ));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van afleveringen voor seizoen {seizoenId}.", ex);
            }
            return items;
        }

        public AfleveringDTO? GetById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ID, SeizoenID, Titel, Afleveringsnummer, Duurtijd FROM Aflevering WHERE ID = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new AfleveringDTO(
                            (int)reader["ID"],
                            (int)reader["SeizoenID"],
                            reader["Titel"]?.ToString() ?? string.Empty,
                            (int)reader["Afleveringsnummer"],
                            (int)reader["Duurtijd"]
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van aflevering {id}.", ex);
            }
            return null;
        }
    }
}