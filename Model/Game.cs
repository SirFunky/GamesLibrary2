using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int PublicherId  { get; set; }
        public int StudioId { get; set; }
        public List<GameDeveloper> GameDevelopers { get; set; }


    }
}
