using Microsoft.Data.SqlClient;
using MovieT.Models;

namespace MovieT.Repositories
{
    public class SerieRepository
    {
        private readonly string _connectionString;

        public SerieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Serie> GetAll()
        {
            var series = new List<Serie>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Titel, Beschrijving FROM Serie", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    series.Add(new Serie
                    {
                        ID = (int)reader["ID"],
                        Titel = reader["Titel"].ToString()!,
                        Beschrijving = reader["Beschrijving"].ToString()!
                    });
                }
            }

            return series;
        }

        public List<Serie> GetByGenre(string genre)
        {
            var series = new List<Serie>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT s.ID, s.Titel, s.Beschrijving 
                      FROM Serie s 
                      JOIN Serie_Genre sg ON s.ID = sg.Serie_ID 
                      JOIN Genre g ON sg.Genre_ID = g.ID 
                      WHERE g.Naam = @genre", con);

                cmd.Parameters.AddWithValue("@genre", genre);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    series.Add(new Serie
                    {
                        ID = (int)reader["ID"],
                        Titel = reader["Titel"].ToString()!,
                        Beschrijving = reader["Beschrijving"].ToString()!
                    });
                }
            }

            return series;
        }
    }
}