using Microsoft.AspNetCore.Mvc;
using ReadCSV_WebApplication.Models;

namespace ReadCSV_WebApplication.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "App_Data", "Movies.csv");

            var movies = ReadMoviesFromSCV(filePath);

            return View(movies);
        }

        private List<MovieModel> ReadMoviesFromSCV(string filePath)
        {
            var movies = new List<MovieModel>();

            if (System.IO.File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    bool isHeader = true;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isHeader)
                        {
                            isHeader = false;
                            continue;
                        }
                        var columns = line.Split(';');

                        if (columns.Length == 4)
                        {
                            movies.Add(new MovieModel
                            {
                                FilmId = int.Parse(columns[0]),
                                FilmName = columns[1],
                                FilmReleaseDate = DateTime.Parse(columns[2]).ToString("yyyy-MM-dd"),
                                FilmOscarWins = int.Parse(columns[3])
                            });
                        }
                    }
                }
            }
            return movies;
        }
    }
}
