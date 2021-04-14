using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWars
{
   
    class Lobby
    {
       private UI uiRef = new UI();
        public void Start()
        {
            Game game = new Game();
            uiRef.ShowSettings();
            game.Start(NumberOfGamesToPlay());
            uiRef.AskForNewGame();
        }
        public int NumberOfGamesToPlay()
        {
            Console.WriteLine("Enter a number of games which you want to play");
            int NumberOFGames = int.Parse(Console.ReadLine());
            Console.Clear();
            return NumberOFGames;
        }

    }
}
