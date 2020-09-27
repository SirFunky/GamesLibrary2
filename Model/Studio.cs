using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GamesLibrary.Model
{
    public class Studio
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<GameDeveloper> GameDevelopers { get; set; }
    }
}
