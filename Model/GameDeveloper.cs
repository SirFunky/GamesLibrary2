﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GamesLibrary.Model
{
    public class GameDeveloper
    {
        public int GameId { get; set; }
        public int DeveloperId { get; set; }
        public int StudioId { get; set; }
        public Game Game { get; set; }
        public Developer Developer { get; set; }
        public Studio studio { get; set; }


    }
}
