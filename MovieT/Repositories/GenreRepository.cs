using Microsoft.Data.SqlClient;
using MovieT.Models;

namespace MovieT.Repositories
{
    public class GenreRepository
    {
        private readonly string _connectionString;

        public GenreRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Genre> GetAll()
        {
            var genres = new List<Genre>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Naam FROM Genre", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    genres.Add(new Genre
                    {
                        ID = (int)reader["ID"],
                        Naam = reader["Naam"].ToString()!
                    });
                }
            }

            return genres;
        }
    }
}