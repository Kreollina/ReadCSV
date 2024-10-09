using System.IO;
using System;

namespace GetAndReadSCV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "Resources", "Movies.csv");

            Console.WriteLine("{0,-4} {1,-83} {2,-15} {3,-10}", "ID", "FilmName", "Release Date", "Oscar Wins");
            Console.WriteLine(new string('-', 115));

            try
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
                            string dateWithoutTime = DateTime.Parse(columns[2]).ToString("yyyy-MM-dd");
                            Console.WriteLine("{0,-4} {1,-83} {2,-15} {3,-10}", columns[0], columns[1], dateWithoutTime, columns[3]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
