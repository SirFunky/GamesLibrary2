﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GamesLibrary.Model
{
    public class Developer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
        public List<GameDeveloper> GameDevelopers { get; set; }

        #region Spel Meny
        public static void UtvecklareMeny()
        {

            bool menuDeveloper = true;
            while (menuDeveloper)
            {
                Console.WriteLine("Lägg till utvecklare (s)");
                Console.WriteLine("Updatera utvecklare (u)");
                Console.WriteLine("Tabort utvecklare (d)");
                Console.WriteLine("Lista utvecklare (l)");
                Console.WriteLine("Gå tillbaka (e)");

                try
                {
                    string menu = Console.ReadLine();

                    switch (menu)
                    {
                        case "s":
                            Console.WriteLine("Var god skriv in namnet på en spel utvecklare. ");
                            string developerName = Console.ReadLine();
                            Console.WriteLine("Var god skriv in en utvecklarens roll. ");
                            string developerRole = Console.ReadLine();
                            CreateDeveloper(developerName, developerRole);
                            ListDevelopers();
                            break;

                        case "u":
                            Console.WriteLine("Var god skriv in utvecklarens namn som du vill uppdatera. ");
                            string updateDeveloper = Console.ReadLine();
                            Console.WriteLine("Skriv in namn. ");
                            string newName = Console.ReadLine();
                            Console.WriteLine("Skriv in roll. ");
                            string newRole = Console.ReadLine();
                            UpdateDeveloper(updateDeveloper, newName, newRole);
                            ListDevelopers();
                            break;

                        case "d":
                            Console.WriteLine("Var god skriv in utvecklarens namn som du vill Radera. ");
                            string deleteDeveloper = Console.ReadLine();
                            DeleteDeveloper(deleteDeveloper);
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
        static void CreateDeveloper(string developerName, string developerRole)
        {
            using (GameContext db = new GameContext())
            {
                Developer newDeveloper = new Developer();
                newDeveloper.Name = developerName;
                newDeveloper.Role = developerRole;
                db.Developers.Add(newDeveloper);
                db.SaveChanges();
            }
        }
        #endregion
        #region Update
        static void UpdateDeveloper(string updateDeveloper, string newName, string newRole)
        {
            using (GameContext db = new GameContext())
            {
                var selectedDeveloper = db.Developers.Where(p => p.Name == updateDeveloper).FirstOrDefault();
                if (selectedDeveloper != null)
                {
                    selectedDeveloper.Name = newName;
                    selectedDeveloper.Role = newRole;
                    db.SaveChanges();
                }
            }
        }
        #endregion
        #region Delete
        static void DeleteDeveloper(string deleteDeveloper)
        {
            using (GameContext db = new GameContext())
            {
                var selectedDeveloper = db.Developers.Where(p => p.Name == deleteDeveloper).FirstOrDefault();
                if (selectedDeveloper != null)
                {
                    db.Developers.Remove(selectedDeveloper);
                    db.SaveChanges();
                }
            }
        }
        #endregion

    }
}
