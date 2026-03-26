using Microsoft.Data.SqlClient;
using MovieT.Models;

namespace MovieT.Repositories
{
    public class FIlmripository
    {
    }
}
namespace MovieT.Repositories
{
    public class FilmRepository
    {
        private readonly string _connectionString;

        public FilmRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Film> GetAll()
        {
            var films = new List<Film>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Titel, Beschrijving FROM Film", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    films.Add(new Film
                    {
                        ID = (int)reader["ID"],
                        Titel = reader["Titel"].ToString()!,
                        Beschrijving = reader["Beschrijving"].ToString()!
                    });
                }
            }
            return films;
        }

        public List<Film> GetByGenre(string genre)
        {
            var films = new List<Film>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT f.ID, f.Titel, f.Beschrijving FROM Film f JOIN Film_Genre fg ON f.ID = fg.Film_ID JOIN Genre g ON fg.Genre_ID = g.ID WHERE g.Naam = @genre", con);
                cmd.Parameters.AddWithValue("@genre", genre);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    films.Add(new Film
                    {
                        ID = (int)reader["ID"],
                        Titel = reader["Titel"].ToString()!,
                        Beschrijving = reader["Beschrijving"].ToString()!
                    });
                }
            }
            return films;
        }
    }
}
