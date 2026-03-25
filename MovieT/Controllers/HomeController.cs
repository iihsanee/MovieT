using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace MovieT.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IActionResult Index(string filter = "alles", string genre = "")
        {
            var items = new List<string>();
            var genres = new List<string>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                // Genres ophalen
                SqlCommand cmdGenre = new SqlCommand("SELECT Naam FROM Genre", con);
                SqlDataReader readerGenre = cmdGenre.ExecuteReader();
                while (readerGenre.Read())
                    genres.Add(readerGenre["Naam"].ToString()!);
                readerGenre.Close();

                // Films ophalen
                if (filter == "alles" || filter == "films" || filter == "genre")
                {
                    string sql = string.IsNullOrEmpty(genre)
                        ? "SELECT Titel, 'FILM' as Type FROM Film"
                        : "SELECT f.Titel, 'FILM' as Type FROM Film f JOIN Film_Genre fg ON f.ID = fg.Film_ID JOIN Genre g ON fg.Genre_ID = g.ID WHERE g.Naam = @genre";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    if (!string.IsNullOrEmpty(genre)) cmd.Parameters.AddWithValue("@genre", genre);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        items.Add("FILM|" + reader["Titel"]);
                    reader.Close();
                }

                // Series ophalen
                if(filter == "alles" || filter == "series" || filter == "genre")
                {
                    string sql = string.IsNullOrEmpty(genre)
                        ? "SELECT Titel, 'SERIE' as Type FROM Serie"
                        : "SELECT s.Titel, 'SERIE' as Type FROM Serie s JOIN Serie_Genre sg ON s.ID = sg.Serie_ID JOIN Genre g ON sg.Genre_ID = g.ID WHERE g.Naam = @genre";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    if (!string.IsNullOrEmpty(genre)) cmd.Parameters.AddWithValue("@genre", genre);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        items.Add("SERIE|" + reader["Titel"]);
                    reader.Close();
                }
            }

            ViewBag.Genres = genres;
            ViewBag.Filter = filter;
            ViewBag.Genre = genre;
            return View(items);
        }
    }
}