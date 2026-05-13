using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAL.DTO;
using Interfaces.Interfaces;

namespace DAL.Repositories
{
    public class SerieRepository : ISerieRepository
    {
        private readonly string _connectionString;

        public SerieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<SerieDTO> GetAll()
        {
            var series = new List<SerieDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Titel, Beschrijving, ReleaseDate, Duration FROM Serie", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SerieDTO(
                        (int)reader["ID"],
                        reader["Titel"]?.ToString() ?? string.Empty,
                        (DateTime)reader["ReleaseDate"],
                        (TimeSpan)reader["Duration"],
                        reader["Beschrijving"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return series;
        }

        public SerieDTO? GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Titel, Beschrijving, ReleaseDate, Duration FROM Serie WHERE ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new SerieDTO(
                        (int)reader["ID"],
                        reader["Titel"]?.ToString() ?? string.Empty,
                        (DateTime)reader["ReleaseDate"],
                        (TimeSpan)reader["Duration"],
                        reader["Beschrijving"]?.ToString() ?? string.Empty
                    );
                }
            }
            return null;
        }

        public List<SerieDTO> Search(string query)
        {
            var series = new List<SerieDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Titel, Beschrijving, ReleaseDate, Duration FROM Serie WHERE Titel LIKE @query", con);
                cmd.Parameters.AddWithValue("@query", "%" + query + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SerieDTO(
                        (int)reader["ID"],
                        reader["Titel"]?.ToString() ?? string.Empty,
                        (DateTime)reader["ReleaseDate"],
                        (TimeSpan)reader["Duration"],
                        reader["Beschrijving"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return series;
        }

        public void AddToWatchingList(int userId, int serieId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO WatchingList (UserID, SerieID) VALUES (@userId, @serieId)", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@serieId", serieId);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddToWatchedList(int userId, int serieId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO WatchedList (UserID, SerieID) VALUES (@userId, @serieId)", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@serieId", serieId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<SerieDTO> GetWatchingList(int userId)
        {
            var series = new List<SerieDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT s.ID, s.Titel, s.Beschrijving, s.ReleaseDate, s.Duration 
                      FROM Serie s 
                      JOIN WatchingList wl ON s.ID = wl.SerieID 
                      WHERE wl.UserID = @userId", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SerieDTO(
                        (int)reader["ID"],
                        reader["Titel"]?.ToString() ?? string.Empty,
                        (DateTime)reader["ReleaseDate"],
                        (TimeSpan)reader["Duration"],
                        reader["Beschrijving"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return series;
        }

        public List<SerieDTO> GetWatchedList(int userId)
        {
            var series = new List<SerieDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT s.ID, s.Titel, s.Beschrijving, s.ReleaseDate, s.Duration 
                      FROM Serie s 
                      JOIN WatchedList wl ON s.ID = wl.SerieID 
                      WHERE wl.UserID = @userId", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SerieDTO(
                        (int)reader["ID"],
                        reader["Titel"]?.ToString() ?? string.Empty,
                        (DateTime)reader["ReleaseDate"],
                        (TimeSpan)reader["Duration"],
                        reader["Beschrijving"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return series;
        }

        public List<SerieDTO> GetTop10Trending()
        {
            var series = new List<SerieDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT TOP 10 s.ID, s.Titel, CAST(s.Beschrijving AS NVARCHAR(MAX)) as Beschrijving, s.ReleaseDate, s.Duration, COUNT(*) as AantalKeerToegevoegd
                      FROM Serie s
                      JOIN WatchingList wl ON s.ID = wl.SerieID
                      GROUP BY s.ID, s.Titel, CAST(s.Beschrijving AS NVARCHAR(MAX)), s.ReleaseDate, s.Duration
                      ORDER BY AantalKeerToegevoegd DESC", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SerieDTO(
                        (int)reader["ID"],
                        reader["Titel"]?.ToString() ?? string.Empty,
                        (DateTime)reader["ReleaseDate"],
                        (TimeSpan)reader["Duration"],
                        reader["Beschrijving"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return series;
        }
    }
}