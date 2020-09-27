using System;
using System.Collections.Generic;
using System.Text;

namespace GamesLibrary.Model
{
    public class GamePublisher
    {
        public int GameId { get; set; }
        public int PublisherId { get; set; }
        public Game Game { get; set; }
        public Publisher publisher { get; set; }
    }
}
