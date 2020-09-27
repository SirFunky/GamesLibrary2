using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int StudioId { get; set; }
        public int GameId { get; set; }

        public List<GameDeveloper> GameDevelopers { get; set; }

    }
}
