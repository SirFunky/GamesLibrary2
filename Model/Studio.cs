using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GamesLibrary.Model
{
    public class Studio
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<GameDeveloper> GameDevelopers { get; set; }

        #region Spel Meny
        public static void StudioMeny()
        {

            bool menuStudio = true;
            while (menuStudio)
            {
                Console.WriteLine("Lägg till Studio (s)");
                Console.WriteLine("Updatera Studio (u)");
                Console.WriteLine("Tabort Studio (d)");
                Console.WriteLine("Lista Studio (l)");
                Console.WriteLine("Gå tillbaka (e)");

                try
                {
                    string menu = Console.ReadLine();

                    switch (menu)
                    {
                        case "s":
                            Console.WriteLine("Var god skriv in namnet på en spel studio. ");
                            string gameStudioName = Console.ReadLine();
                            CreateStudio(gameStudioName);
                            ListStudios();
                            break;

                        case "u":
                            Console.WriteLine("Var god skriv in namnet på en spel studio som du vill uppdatera. ");
                            string updateStudio = Console.ReadLine();
                            Console.WriteLine("Skriv in namn. ");
                            string newName = Console.ReadLine();
                            UpdateStudio(updateStudio, newName);
                            ListStudios();
                            break;

                        case "d":
                            Console.WriteLine("Var god skriv in studio namn som du vill Radera. ");
                            string deleteStudio = Console.ReadLine();
                            DeleteStudio(deleteStudio);
                            ListStudios();
                            break;

                        case "l":
                            ListStudios();
                            break;

                        case "e":
                            menuStudio = false;
                            break;

                        default:
                            menuStudio = true;
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
        static public void ListStudios()
        {
            using (GameContext db = new GameContext())
            {
                List<Studio> StudioList = db.Studios
                    .ToList();

                foreach (var Studio in StudioList)
                {
                    Console.WriteLine(Studio.Name);
                }
            }
        }
        #endregion
        #region Create
        static void CreateStudio(string gameStudioName)
        {
            using (GameContext db = new GameContext())
            {
                Studio newPublisher = new Studio();
                newPublisher.Name = gameStudioName;
                db.Studios.Add(newPublisher);
                db.SaveChanges();
            }
        }
        #endregion
        #region Update
        static void UpdateStudio(string updateStudio, string newName)
        {
            using (GameContext db = new GameContext())
            {
                var selectedPublisher = db.Studios.Where(p => p.Name == updateStudio).FirstOrDefault();
                if (selectedPublisher != null)
                {
                    selectedPublisher.Name = newName;
                    db.SaveChanges();
                }
            }
        }
        #endregion
        #region Delete
        static void DeleteStudio(string deleteStudio)
        {
            using (GameContext db = new GameContext())
            {
                var selectedStudio = db.Studios.Where(p => p.Name == deleteStudio).FirstOrDefault();
                if (selectedStudio != null)
                {
                    db.Studios.Remove(selectedStudio);
                    db.SaveChanges();
                }
            }
        }
        #endregion
    }
}
