using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GamesLibrary.Model
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<GamePublisher> GamePublishers { get; set; }

        #region Spel Meny
        public static void UtgivareMeny()
        {

            bool menuDeveloper = true;
            while (menuDeveloper)
            {
                Console.WriteLine("Lägg till Utgivare (s)");
                Console.WriteLine("Updatera Utgivare (u)");
                Console.WriteLine("Tabort Utgivare (d)");
                Console.WriteLine("Lista Utgivare (l)");
                Console.WriteLine("Gå tillbaka (e)");

                try
                {
                    string menu = Console.ReadLine();

                    switch (menu)
                    {
                        case "s":
                            Console.WriteLine("Var god skriv in namnet på en spel utgivare. ");
                            string gamePublisherName = Console.ReadLine();
                            CreatePublisher(gamePublisherName);
                            ListDevelopers();
                            break;

                        case "u":
                            Console.WriteLine("Var god skriv in namnet på en spel utgivare som du vill uppdatera. ");
                            string updateDeveloper = Console.ReadLine();
                            Console.WriteLine("Skriv in namn. ");
                            string newName = Console.ReadLine();
                            UpdateGame(updateDeveloper, newName);
                            ListDevelopers();
                            break;

                        case "d":
                            Console.WriteLine("Var god skriv in utgivares namn som du vill Radera. ");
                            string deleteDeveloper = Console.ReadLine();
                            DeleteGame(deleteDeveloper);
                            ListDevelopers();
                            break;

                        case "l":
                            ListDevelopers();
                            break;

                        case "e":
                            menuDeveloper = false;
                            break;

                        default:
                            menuDeveloper = true;
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
        static public void ListDevelopers()
        {
            using (GameContext db = new GameContext())
            {
                List<Developer> developerList = db.Developers
                    .Include(g => g.GameDevelopers)
                    .ThenInclude(d => d.Developer)
                    .Include(st => st.GameDevelopers)
                    .ThenInclude(s => s.studio)
                    .ToList();

                foreach (var Developer in developerList)
                {
                    string studio = "";
                    foreach (var gameStudio in Developer.GameDevelopers)
                    {
                        studio += gameStudio.studio.Name + " ";
                    }

                    Console.WriteLine(Developer.Name + " " + Developer.Role + " " + studio + " ");
                }
            }
        }
        #endregion
        #region Create
        static void CreatePublisher(string gamePublisherName)
        {
            using (GameContext db = new GameContext())
            {
                Developer newPublisher = new Developer();
                newPublisher.Name = gamePublisherName;
                db.Publishers.Add(newPublisher);
                db.SaveChanges();
            }
        }
        #endregion
        #region Update
        static void UpdateGame(string updateDeveloper, string newName)
        {
            using (GameContext db = new GameContext())
            {
                var selectedPublisher = db.Developers.Where(p => p.Name == updateDeveloper).FirstOrDefault();
                if (selectedPublisher != null)
                {
                    selectedPublisher.Name = newName;
                    db.SaveChanges();
                }
            }
        }
        #endregion
        #region Delete
        static void DeleteGame(string deleteDeveloper)
        {
            using (GameContext db = new GameContext())
            {
                var selectedGame = db.Developers.Where(p => p.Name == deleteDeveloper).FirstOrDefault();
                if (selectedGame != null)
                {
                    db.Developers.Remove(selectedGame);
                    db.SaveChanges();
                }
            }
        }
        #endregion
    }
}
