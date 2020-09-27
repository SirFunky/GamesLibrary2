using GamesLibrary.Model;
using System;


namespace GamesLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            bool huvudMenu = true;
            while (huvudMenu)
            {
                Console.WriteLine("Konfigurera spel. (s)");
                Console.WriteLine("Konfigurera utvecklare. (u)");
                Console.WriteLine("Konfigurera studio. (d)");
                Console.WriteLine("Konfigurera utgivare. (g)");
                Console.WriteLine("Lista alla spel. (l)");
                Console.WriteLine("Avsluta (e)");

                try
                {
                    string menu = Console.ReadLine();

                    switch (menu)
                    {
                        case "s":
                            Game.Spelmeny();
                            break;

                        case "u":
                            Developer.UtvecklareMeny();
                            break;

                        case "d":
                            Studio.StudioMeny();
                            break;

                        case "g":
                            Publisher.UtgivareMeny();
                            break;

                        case "l":
                            Game.ListGames();
                            break;

                        case "e":
                            huvudMenu = false;
                            break;

                        default:
                            huvudMenu = true;
                            break;
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
