using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAL.DTO;

namespace DAL.Repositories
{
    public class FilmModelRepository : IFilmModelRepository
    {
        private readonly string _connectionString;

        public FilmModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<FilmModelDTO> GetAll()
        {
            var FilmModels = new List<FilmModelDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Titel, Beschrijving, ReleaseDate, Duration FROM Film", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FilmModels.Add(new FilmModelDTO
                    {
                        Id = (int)reader["ID"],
                        Title = reader["Titel"].ToString(),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Duration = (TimeSpan)reader["Duration"],
                        Description = reader["Beschrijving"].ToString()
                    });
                }
            }
            return FilmModels;
        }

        public FilmModelDTO GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Titel, Beschrijving, ReleaseDate, Duration FROM Film WHERE ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new FilmModelDTO
                    {
                        Id = (int)reader["ID"],
                        Title = reader["Titel"].ToString(),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Duration = (TimeSpan)reader["Duration"],
                        Description = reader["Beschrijving"].ToString()
                    };
                }
            }
            return null;
        }

        public List<FilmModelDTO> Search(string query)
        {
            var FilmModels = new List<FilmModelDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Titel, Beschrijving, ReleaseDate, Duration FROM Film WHERE Titel LIKE @query", con);
                cmd.Parameters.AddWithValue("@query", "%" + query + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FilmModels.Add(new FilmModelDTO
                    {
                        Id = (int)reader["ID"],
                        Title = reader["Titel"].ToString(),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Duration = (TimeSpan)reader["Duration"],
                        Description = reader["Beschrijving"].ToString()
                    });
                }
            }
            return FilmModels;
        }

        public void AddToWatchingList(int userId, int FilmModelId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO WatchingList (UserID, FilmModelID) VALUES (@userId, @FilmModelId)", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@FilmModelId", FilmModelId);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddToWatchedList(int userId, int FilmModelId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO WatchedList (UserID, FilmModelID) VALUES (@userId, @FilmModelId)", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@FilmModelId", FilmModelId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<FilmModelDTO> GetWatchingList(int userId)
        {
            var FilmModels = new List<FilmModelDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT f.ID, f.Titel, f.Beschrijving, f.ReleaseDate, f.Duration 
                      FROM Film f 
                      JOIN WatchingList wl ON f.ID = wl.FilmModelID 
                      WHERE wl.UserID = @userId", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FilmModels.Add(new FilmModelDTO
                    {
                        Id = (int)reader["ID"],
                        Title = reader["Titel"].ToString(),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Duration = (TimeSpan)reader["Duration"],
                        Description = reader["Beschrijving"].ToString()
                    });
                }
            }
            return FilmModels;
        }

        public List<FilmModelDTO> GetWatchedList(int userId)
        {
            var FilmModels = new List<FilmModelDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT f.ID, f.Titel, f.Beschrijving, f.ReleaseDate, f.Duration 
                      FROM Film f 
                      JOIN WatchedList wl ON f.ID = wl.FilmModelID 
                      WHERE wl.UserID = @userId", con);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FilmModels.Add(new FilmModelDTO
                    {
                        Id = (int)reader["ID"],
                        Title = reader["Titel"].ToString(),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Duration = (TimeSpan)reader["Duration"],
                        Description = reader["Beschrijving"].ToString()
                    });
                }
            }
            return FilmModels;
        }

        public List<FilmModelDTO> GetTop10Trending()
        {
            var films = new List<FilmModelDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT TOP 10 f.ID, f.Titel, CAST(f.Beschrijving AS NVARCHAR(MAX)) as Beschrijving, f.ReleaseDate, f.Duration, COUNT(*) as AantalKeerToegevoegd
                      FROM Film f
                      JOIN WatchingList wl ON f.ID = wl.FilmModelID
                      GROUP BY f.ID, f.Titel, CAST(f.Beschrijving AS NVARCHAR(MAX)), f.ReleaseDate, f.Duration
                      ORDER BY AantalKeerToegevoegd DESC", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    films.Add(new FilmModelDTO
                    {
                        Id = (int)reader["ID"],
                        Title = reader["Titel"].ToString(),
                        Description = reader["Beschrijving"].ToString(),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Duration = (TimeSpan)reader["Duration"]
                    });
                }
            }
            return films;
        }
    }
}