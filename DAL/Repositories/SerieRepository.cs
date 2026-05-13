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
            try
            {
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
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij ophalen van alle series.", ex);
            }
            return series;
        }

        public SerieDTO? GetById(int id)
        {
            try
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
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van serie met ID {id}.", ex);
            }
            return null;
        }

        public List<SerieDTO> Search(string query)
        {
            var series = new List<SerieDTO>();
            try
            {
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
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij zoeken naar series met query '{query}'.", ex);
            }
            return series;
        }

        public void AddToWatchingList(int userId, int serieId)
        {
            try
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
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij toevoegen van serie aan watchinglist.", ex);
            }
        }

        public void AddToWatchedList(int userId, int serieId)
        {
            try
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
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij toevoegen van serie aan watchedlist.", ex);
            }
        }

        public List<SerieDTO> GetWatchingList(int userId)
        {
            var series = new List<SerieDTO>();
            try
            {
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
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van watchinglist voor gebruiker {userId}.", ex);
            }
            return series;
        }

        public List<SerieDTO> GetWatchedList(int userId)
        {
            var series = new List<SerieDTO>();
            try
            {
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
            }
            catch (SqlException ex)
            {
                throw new Exception($"Databasefout bij ophalen van watchedlist voor gebruiker {userId}.", ex);
            }
            return series;
        }

        public List<SerieDTO> GetTop10Trending()
        {
            var series = new List<SerieDTO>();
            try
            {
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
            }
            catch (SqlException ex)
            {
                throw new Exception("Databasefout bij ophalen van top 10 trending series.", ex);
            }
            return series;
        }
    }
}