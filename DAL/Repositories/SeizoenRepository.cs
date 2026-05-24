using Microsoft.Data.SqlClient;
using DAL.DTO;
using Interfaces.Interfaces;

namespace DAL.Repositories
{
    public class SeizoenRepository : ISeizoenRepository
    {
        private readonly string _connectionString;

        public SeizoenRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<SeizoenDTO> GetBySerieId(int serieId)
        {
            var items = new List<SeizoenDTO>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ID, SerieID, Seizoennummer, AantalAfleveringen, Jaartal FROM Seizoen WHERE SerieID = @serieId", con);
                    cmd.Parameters.AddWithValue("@serieId", serieId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        items.Add(new SeizoenDTO(
                            (int)reader["ID"],
                            (int)reader["SerieID"],
                            (int)reader["Seizoennummer"],
                            (int)reader["AantalAfleveringen"],
                            (int)reader["Jaartal"]
                        ));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van seizoenen voor serie {serieId}.", ex);
            }
            return items;
        }

        public SeizoenDTO? GetById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT ID, SerieID, Seizoennummer, AantalAfleveringen, Jaartal FROM Seizoen WHERE ID = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new SeizoenDTO(
                            (int)reader["ID"],
                            (int)reader["SerieID"],
                            (int)reader["Seizoennummer"],
                            (int)reader["AantalAfleveringen"],
                            (int)reader["Jaartal"]
                        );
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van seizoen {id}.", ex);
            }
            return null;
        }
    }
}