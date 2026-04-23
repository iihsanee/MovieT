using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAL.DTO;

namespace DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly string _connectionString;

        public GenreRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<GenreDTO> GetAll()
        {
            var genres = new List<GenreDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Naam FROM Genre", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreDTO(
                        (int)reader["ID"],
                        reader["Naam"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return genres;
        }

        public GenreDTO? GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Naam FROM Genre WHERE ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new GenreDTO(
                        (int)reader["ID"],
                        reader["Naam"]?.ToString() ?? string.Empty
                    );
                }
            }
            return null;
        }

        public List<GenreDTO> GetByFilmId(int filmId)
        {
            var genres = new List<GenreDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT g.ID, g.Naam FROM Genre g
                      JOIN Film_Genre fg ON g.ID = fg.Genre_ID
                      WHERE fg.Film_ID = @filmId", con);
                cmd.Parameters.AddWithValue("@filmId", filmId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreDTO(
                        (int)reader["ID"],
                        reader["Naam"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return genres;
        }

        public List<GenreDTO> GetBySerieId(int serieId)
        {
            var genres = new List<GenreDTO>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT g.ID, g.Naam FROM Genre g
                      JOIN Serie_Genre sg ON g.ID = sg.Genre_ID
                      WHERE sg.Serie_ID = @serieId", con);
                cmd.Parameters.AddWithValue("@serieId", serieId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new GenreDTO(
                        (int)reader["ID"],
                        reader["Naam"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return genres;
        }
    }
}