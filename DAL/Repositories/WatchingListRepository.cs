using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAL.DTO;

namespace DAL.Repositories
{
    public class WatchingListRepository : IWatchingListRepository
    {
        private readonly string _connectionString;

        public WatchingListRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<WatchingListDTO> GetByUser(int userId)
        {
            var items = new List<WatchingListDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT wl.UserID, wl.FilmModelID, wl.SerieID,
                      CASE WHEN wl.FilmModelID IS NOT NULL THEN f.Titel ELSE s.Titel END as Titel,
                      CASE WHEN wl.FilmModelID IS NOT NULL THEN 'Film' ELSE 'Serie' END as Type
                      FROM WatchingList wl
                      LEFT JOIN Film f ON wl.FilmModelID = f.ID
                      LEFT JOIN Serie s ON wl.SerieID = s.ID
                      WHERE wl.UserID = @userId", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(new WatchingListDTO(
                        (int)reader["UserID"],
                        reader["FilmModelID"] == DBNull.Value ? null : (int?)reader["FilmModelID"],
                        reader["SerieID"] == DBNull.Value ? null : (int?)reader["SerieID"],
                        reader["Titel"]?.ToString() ?? string.Empty,
                        reader["Type"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return items;
        }

        public void Add(int userId, int? filmId, int? serieId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO WatchingList (UserID, FilmModelID, SerieID) VALUES (@userId, @filmId, @serieId)", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@filmId", filmId.HasValue ? (object)filmId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@serieId", serieId.HasValue ? (object)serieId.Value : DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
    }
}