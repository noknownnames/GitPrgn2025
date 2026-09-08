using System.Security.Cryptography;

namespace ConsoleApp1
{
    internal class Program
    {
        class Film
        {
            public Film(string name, string director_name, string director_surname, int year, float num )
            {
                Name = name;
                DirectorName = director_name;
                DirectorSurname = director_surname;
                Year = year;
                Score = num;
                NumOfRatings = 1;
            }
            public string Name {  get; set; }
            public string DirectorName{ get; set; }
            public string DirectorSurname{ get; set; }
            public int Year { get; set; }
            public float Score { get; set; }
            private int NumOfRatings { get; set; }
            public void Rate(int newRating)
            {
                if(newRating < 0)
                {
                    newRating = 0;
                }
                if (newRating > 5)
                {
                    newRating = 5;
                }
                NumOfRatings += 1;
                Score = (Score*(NumOfRatings-1) + newRating) / NumOfRatings;
            }
            override public string ToString()
            {
                return ($"{Name} ({Year}; {DirectorName[0]}.{DirectorSurname} ) Score: {Score}/5");
            }
        }
        static void Main(string[] args)
        {
            Film jumanji = new Film("Jumanji","Jake","Kasdan", 2017, 3.6f);
            Film fastandfurious6 = new Film("Fast & Furious 6", "Justin", "Lin", 2013, 3.55f);
            Film moana = new Film("Moana", "Thomas", "Kail", 2026, 3.35f);
            List<Film> films = new List<Film>(){jumanji, fastandfurious6, moana};

            foreach (Film film in films)
            {
                for (int i = 0; i < 15; i++)
                {
                    film.Rate(new Random().Next(0, 6));
                }
            }
            foreach (Film film in films)
            {
                Console.Write(film.ToString());
                if (film.Score < 3)
                {
                    Console.Write($", tudíž {film.Name} je odpad, neboť na film s hodnocením {film.Score} by se žádná důstojná osoba dívat neměla!\n");
                }
                else
                {
                    Console.Write("\n");
                }
            }
            Console.Write("\n");
            Film filmWithBestScore = jumanji;
            foreach (Film film in films)
            {
                if (filmWithBestScore.Score < film.Score)
                {
                    filmWithBestScore = film;
                }
            }
            Console.Write($"film s nejlepším hodnocením je {filmWithBestScore}");
            Console.Write("\n");
            Film filmWithLongestName = jumanji;
            foreach (Film film in films)
            {
               if (filmWithLongestName.Name.Length < film.Name.Length)
               {
               filmWithLongestName = film;
               }
            }
            Console.Write($"film s nejdelším názvem je {filmWithLongestName}");
            Console.Write("\n");
        }
    }
}
