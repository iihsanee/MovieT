using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAL.DTO;

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
                    items.Add(new WatchedListDTO
                    {
                        UserId = (int)reader["UserID"],
                        FilmId = reader["FilmModelID"] == DBNull.Value ? null : (int?)reader["FilmModelID"],
                        SerieId = reader["SerieID"] == DBNull.Value ? null : (int?)reader["SerieID"],
                        Title = reader["Titel"].ToString(),
                        Type = reader["Type"].ToString()
                    });
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
                    "INSERT INTO WatchedList (UserID, FilmModelID, SerieID) VALUES (@userId, @filmId, @serieId)", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@filmId", filmId.HasValue ? (object)filmId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@serieId", serieId.HasValue ? (object)serieId.Value : DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
    }
}