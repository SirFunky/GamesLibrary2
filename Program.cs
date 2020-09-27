using GamesLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamesLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            bool test = true;
            while (test)
            {
                Console.WriteLine("Lägg till spel (s)");
                Console.WriteLine("Updatera spel (u)");
                Console.WriteLine("Tabort spel (d)");
                Console.WriteLine("Lista spel (l)");
                Console.WriteLine("Avsluta (e)");

                try
                {
                    string menu = Console.ReadLine();

                    switch (menu)
                    {
                        case "s":
                            Console.WriteLine("Var god skriv in spelets namn. ");
                            string gameName = Console.ReadLine();
                            Console.WriteLine("Var god skriv in spelets gener. ");
                            string gameGenre = Console.ReadLine();
                            Console.WriteLine("Var god skriv in en av spelets utvecklare. ");
                            string gameDeveloper = Console.ReadLine();
                            Console.WriteLine("Var god skriv in en utvecklarens roll. ");
                            string developerRole = Console.ReadLine();
                            CreateGame(gameName, gameGenre, gameDeveloper, developerRole);
                            ListGames();
                            break;

                        case "u":
                            Console.WriteLine("Var god skriv in spelets namn som du vill uppdatera. ");
                            string updateGame = Console.ReadLine();
                            Console.WriteLine("Skriv in namn. ");
                            string newName = Console.ReadLine();
                            Console.WriteLine("Skriv in genre. ");
                            string newGenre = Console.ReadLine();
                            UpdateGame(updateGame,newName,newGenre);
                            ListGames();
                            break;

                        case "d":
                            Console.WriteLine("Var god skriv in spelets namn som du vill Radera. ");
                            string deleteGame = Console.ReadLine();
                            DeleteGame(deleteGame);
                            ListGames();
                            break;

                        case "l":
                            ListGames();
                            break;

                        case "e":
                            test = false;
                            break;

                        default:
                            test = true;
                            break;
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
        #region Read
        static private void ListGames()
        {
            using (GameContext db = new GameContext())
            {
                List<Game> gameList = db.Games
                    .Include(g => g.GameDevelopers)
                    .ThenInclude(d => d.Developer)
                    .ToList();

                foreach (var game in gameList)
                {
                    string developers = "";
                    foreach (var gameDeveloper in game.GameDevelopers)
                    {
                        developers += gameDeveloper.Developer.Name + " ";
                    }
                    Console.WriteLine(game.Name + " " + game.Gener + " " + developers);
                }
            }
        }
        #endregion
        #region Create
        static void CreateGame(string name, string gener, string developerName, string developerRole)
        {
            using (GameContext db = new GameContext())
            {
                Developer developer = db.Developers.Where(n => n.Name == developerName).FirstOrDefault();
                if (developer != null)
                {
                    Game newGame = new Game();
                    newGame.Gener = gener;
                    newGame.Name = name;
                    db.Games.Add(newGame);
                    db.SaveChanges(); //Borde inte behövas väl?

                    GameDeveloper gameDeveloper = new GameDeveloper();
                    gameDeveloper.DeveloperId = developer.Id;
                    gameDeveloper.GameId = newGame.Id;
                    db.GameDevelopers.Add(gameDeveloper);
                    db.SaveChanges();
                }
                else
                {
                    Game newGame = new Game();
                    newGame.Gener = gener;
                    newGame.Name = name;
                    db.Games.Add(newGame);
                    db.SaveChanges(); //Borde inte behövas väl?

                    Developer newDeveloper = new Developer();
                    newDeveloper.Name = developerName;
                    newDeveloper.Role = developerRole;
                    db.Developers.Add(newDeveloper);
                    db.SaveChanges(); //Borde inte behövas väl?

                    GameDeveloper gameDeveloper = new GameDeveloper();
                    gameDeveloper.DeveloperId = newDeveloper.Id;
                    gameDeveloper.GameId = newGame.Id;
                    db.GameDevelopers.Add(gameDeveloper);
                    db.SaveChanges();
                }
            }
        }
        #endregion
        #region Update
        static void UpdateGame(string updateGame, string newName, string newGenre)
        {
            using (GameContext db = new GameContext())
            {
                var selectedGame = db.Games.Where(p => p.Name == updateGame).FirstOrDefault();
                if (selectedGame != null)
                {
                    selectedGame.Name = newName;
                    selectedGame.Gener = newGenre;
                    db.SaveChanges();
                }
            }
        }
        #endregion
        #region Delete
        static void DeleteGame(string deleteGame)
        {
            using (GameContext db = new GameContext())
            {
                var selectedGame = db.Games.Where(p => p.Name == deleteGame).FirstOrDefault();
                if (selectedGame != null)
                {
                    db.Games.Remove(selectedGame);
                    db.SaveChanges();
                }
            }
        }
        #endregion
    }
}
