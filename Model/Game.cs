using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GamesLibrary.Model
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        public string Gener { get; set; }
        [Required]
        public int  NumberOfPlayers { get; set; }
        public Publisher Publisher { get; set; }
        public List<GameDeveloper> GameDevelopers { get; set; }

        #region Spel Meny
        public static void Spelmeny()
        {

            bool menySpel = true;
            while (menySpel)
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
                            Console.WriteLine("Var god skriv in hur många spelare spelet har. ");
                            int numberOfPlayers = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Var god skriv in en av spelets utvecklare. ");
                            string gameDeveloper = Console.ReadLine();
                            Console.WriteLine("Var god skriv in en utvecklarens roll. ");
                            string developerRole = Console.ReadLine();
                            Console.WriteLine("Var god skriv in vilken spel studio som skapat spelet. ");
                            string studioName = Console.ReadLine();
                            Console.WriteLine("Var god skriv in vilken utgivare som gett ut spelet. ");
                            string publisherName = Console.ReadLine();
                            CreateGame(gameName, gameGenre, gameDeveloper, developerRole, studioName, numberOfPlayers, publisherName);
                            ListGames();
                            break;

                        case "u":
                            Console.WriteLine("Var god skriv in spelets namn som du vill uppdatera. ");
                            string updateGame = Console.ReadLine();
                            Console.WriteLine("Skriv in namn. ");
                            string newName = Console.ReadLine();
                            Console.WriteLine("Skriv in genre. ");
                            string newGenre = Console.ReadLine();
                            UpdateGame(updateGame, newName, newGenre);
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
                            menySpel = false;
                            break;

                        default:
                            menySpel = true;
                            break;
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
        #endregion
        #region Read
        static public void ListGames()
        {
            using (GameContext db = new GameContext())
            {
                List<Game> gameList = db.Games
                    .Include(g => g.GameDevelopers)
                    .ThenInclude(d => d.Developer)
                    .Include(st => st.GameDevelopers)
                    .ThenInclude(s => s.studio)
                    .Include (p => p.Publisher)
                    .ToList();

                foreach (var game in gameList)
                {
                    string developers = "";
                    foreach (var gameDeveloper in game.GameDevelopers)
                    {
                        developers += gameDeveloper.Developer.Name + " ";
                    }
                    string studio = "";
                    foreach (var gamedeveloper in game.GameDevelopers)
                    {
                        studio += gamedeveloper.studio.Name + " ";
                    }

                    Console.WriteLine(game.Name + " " + game.Gener + " " + game.NumberOfPlayers + " " + developers + " " + studio + " " + game.Publisher.Name);
                }
            }
        }
        #endregion
        #region Create
        static void CreateGame(string name, string gener, string developerName, string developerRole, string studioName, int numberOfPlayers, string publisherName)
        {
            using (GameContext db = new GameContext())
            {
                Developer developer = db.Developers.Where(n => n.Name == developerName).FirstOrDefault();
                Studio studio = db.Studios.Where(s => s.Name == studioName).FirstOrDefault();
                Publisher publisher = db.Publishers.Where(p => p.Name == publisherName).FirstOrDefault();
               
                if (developer != null && studio != null && publisher != null)
                {
                    Game newGame = new Game();
                    newGame.Gener = gener;
                    newGame.Name = name;
                    newGame.NumberOfPlayers = numberOfPlayers;
                    newGame.Publisher.Id = publisher.Id;
                    db.Games.Add(newGame);
                    db.SaveChanges();

                    GameDeveloper gameDeveloper = new GameDeveloper();
                    gameDeveloper.StudioId = studio.Id;
                    gameDeveloper.DeveloperId = developer.Id;
                    gameDeveloper.GameId = newGame.Id;
                    db.GameDevelopers.Add(gameDeveloper);
                    db.SaveChanges();
                }
                else if ( studio != null && publisher != null)
                {
                    Game newGame = new Game();
                    newGame.Gener = gener;
                    newGame.Name = name;
                    newGame.NumberOfPlayers = numberOfPlayers;
                    newGame.Publisher.Id = publisher.Id;
                    db.Games.Add(newGame);
                    db.SaveChanges();

                    Developer newDeveloper = new Developer();
                    newDeveloper.Name = developerName;
                    newDeveloper.Role = developerRole;
                    db.Developers.Add(newDeveloper);
                    db.SaveChanges();

                    GameDeveloper gameDeveloper = new GameDeveloper();
                    gameDeveloper.DeveloperId = newDeveloper.Id;
                    gameDeveloper.GameId = newGame.Id;
                    gameDeveloper.StudioId = studio.Id;
                    db.GameDevelopers.Add(gameDeveloper);
                    db.SaveChanges();
                }
                else if ( developer != null && publisher != null)
                {
                    Game newGame = new Game();
                    newGame.Gener = gener;
                    newGame.Name = name;
                    newGame.NumberOfPlayers = numberOfPlayers;
                    newGame.Publisher.Id = publisher.Id;
                    db.Games.Add(newGame);
                    db.SaveChanges();

                    Studio newStudio = new Studio();
                    newStudio.Name = studioName;
                    db.Studios.Add(newStudio);
                    db.SaveChanges();

                    GameDeveloper gameDeveloper = new GameDeveloper();
                    gameDeveloper.DeveloperId = newStudio.Id;
                    gameDeveloper.GameId = newGame.Id;
                    gameDeveloper.StudioId = developer.Id;
                    db.GameDevelopers.Add(gameDeveloper);
                    db.SaveChanges();
                }
                else if (developer != null && studio != null)
                {
                    Game newGame = new Game();
                    newGame.Gener = gener;
                    newGame.Name = name;
                    newGame.NumberOfPlayers = numberOfPlayers;
                    newGame.Publisher.Id = publisher.Id;
                    db.Games.Add(newGame);
                    db.SaveChanges();

                    Publisher newPublisher = new Publisher();
                    newPublisher.Name = publisherName;
                    db.Publishers.Add(newPublisher);
                    db.SaveChanges();

                    GameDeveloper gameDeveloper = new GameDeveloper();
                    gameDeveloper.StudioId = studio.Id;
                    gameDeveloper.DeveloperId = developer.Id;
                    gameDeveloper.GameId = newGame.Id;
                    db.GameDevelopers.Add(gameDeveloper);
                    db.SaveChanges();
                }

                else
                {


                    Studio newStudio = new Studio();
                    newStudio.Name = studioName;
                    db.Studios.Add(newStudio);
                    db.SaveChanges();

                    Developer newDeveloper = new Developer();
                    newDeveloper.Name = developerName;
                    newDeveloper.Role = developerRole;
                    db.Developers.Add(newDeveloper);
                    db.SaveChanges();

                    Publisher newPublisher = new Publisher();
                    newPublisher.Name = publisherName;
                    db.Publishers.Add(newPublisher);
                    db.SaveChanges();

                    Game newGame = new Game();
                    newGame.Gener = gener;
                    newGame.Name = name;
                    newGame.NumberOfPlayers = numberOfPlayers;
                    newGame.Publisher.Id = newPublisher.Id;
                    db.Games.Add(newGame);
                    db.SaveChanges();

                    GameDeveloper gameDeveloper = new GameDeveloper();
                    gameDeveloper.StudioId = newStudio.Id;
                    gameDeveloper.GameId = newGame.Id;
                    gameDeveloper.DeveloperId = newDeveloper.Id;
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

