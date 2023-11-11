using Newtonsoft.Json;
using System.Globalization;
using System.Xml.Linq;

namespace DZ_c_dies_776
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string XmlString = "C:\\Users\\User\\Desktop\\C#\\DZ_c_dies_776\\DZ_c_dies_776\\movies.xml";
            XDocument doc = XDocument.Load(XmlString);

            var FILM = from f in doc.Descendants("movie")
                       select new
                       {
                           title = Convert.ToString(f.Element("title")?.Value),
                           genre = Convert.ToString(f.Element("genre")?.Value),
                           year = Convert.ToInt32(f.Element("year")?.Value),
                           rating = float.Parse(f.Element("rating")?.Value, CultureInfo.InvariantCulture.NumberFormat)

        };
            int a = 0;

            var selectedFILM = from s in FILM
                                orderby s.title
                                select s;
            foreach (var FF in selectedFILM)
            {
                ++a;
                Console.WriteLine(a);
                Console.Write(" Название " + FF.title);           
                Console.WriteLine();
            }
            SortedSet<string> genres = new SortedSet<string>();
            foreach (var fi in selectedFILM)
            {
                genres.Add(fi.genre);
            }
            foreach(var G in genres)
            {
                Console.WriteLine(G);
            }
            Console.WriteLine("\n Общее количество фильмов : " + a);
            var RAT=selectedFILM.Max(fi => Convert.ToDouble(fi.rating));
            var RRAT = selectedFILM.Where(f => Convert.ToDouble(f.rating) == RAT).Select(f => f);
            foreach(var R in RRAT)
            {
                Console.WriteLine("\n Фильм с самым большим рейтингом : " + R.title + " его рейтинг " + R.rating);
            }
            var SORTfilm = FILM.OrderByDescending(f => f.year).Select(f => f);
            foreach(var SO in SORTfilm)
            {
                Console.WriteLine(SO.title + "  " + SO.year);
            }
            var FILM_5 = selectedFILM.Where(f => f.year >= 2018).Select(f => f);
            foreach(var F in FILM_5)
            {
                Console.WriteLine(" Фильмы за последние пять лет : " + F.title+"  "+F.year);
            }
            var SrR = selectedFILM.Sum(f => Convert.ToDouble(f.rating)) / a;
            Console.WriteLine("Средний рейтинг : " + SrR);
            var FILM_2010 = selectedFILM.Where(f => f.year >= 2010 && Convert.ToDouble(f.rating) >= 8).Select(f => f);
            Console.WriteLine("\n Фильмы ,выпущенные после 2010 и имеющие рейтинг больше 8 : ");
            foreach(var F in FILM_2010)
            {
                Console.WriteLine(F.title + "   " + F.rating);
            }

            string fileName = "C:\\Users\\User\\Desktop\\C#\\DZ_c_dies_776\\DZ_c_dies_776\\output.json";
            string json = JsonConvert.SerializeObject(selectedFILM, Formatting.Indented);

            string json1 = JsonConvert.SerializeObject(genres, Formatting.Indented);

            string json2 = JsonConvert.SerializeObject(RRAT, Formatting.Indented);

            string json3 = JsonConvert.SerializeObject(SORTfilm, Formatting.Indented);

            string json4 = JsonConvert.SerializeObject(FILM_2010, Formatting.Indented);

            json = json + json1 + json2 + json3 + json4;
            System.IO.File.WriteAllText(fileName, json);

            Console.WriteLine(File.ReadAllText(fileName));

        }
    }
}