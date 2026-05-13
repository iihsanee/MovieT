using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAL.DTO;
using Interfaces.Interfaces;

namespace DAL.Repositories
{
    public class WatchedListRepository : IWatchedListRepository
    {
        private readonly string _connectionString;

        public WatchedListRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<WatchedListDTO> GetByUser(int userId)
        {
            var items = new List<WatchedListDTO>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT wl.UserID, wl.FilmModelID, wl.SerieID,
                          CASE WHEN wl.FilmModelID IS NOT NULL THEN f.Titel ELSE s.Titel END as Titel,
                          CASE WHEN wl.FilmModelID IS NOT NULL THEN 'Film' ELSE 'Serie' END as Type
                          FROM WatchedList wl
                          LEFT JOIN Film f ON wl.FilmModelID = f.ID
                          LEFT JOIN Serie s ON wl.SerieID = s.ID
                          WHERE wl.UserID = @userId", con);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        items.Add(new WatchedListDTO(
                            (int)reader["UserID"],
                            reader["FilmModelID"] == DBNull.Value ? null : (int?)reader["FilmModelID"],
                            reader["SerieID"] == DBNull.Value ? null : (int?)reader["SerieID"],
                            reader["Titel"]?.ToString() ?? string.Empty,
                            reader["Type"]?.ToString() ?? string.Empty
                        ));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van watchedlist voor gebruiker {userId}.", ex);
            }
            return items;
        }

        public void Add(int userId, int? filmId, int? serieId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO WatchedList (UserID, FilmModelID, SerieID) VALUES (@userId, @filmId, @serieId)", con);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@filmId", filmId.HasValue ? (object)filmId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@serieId", serieId.HasValue ? (object)serieId.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij toevoegen aan watchedlist.", ex);
            }
        }
    }
}