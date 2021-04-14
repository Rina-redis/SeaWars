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
            int NumberOfGames;
            Console.WriteLine("Enter a number of games which you want to play");
             bool result = int.TryParse(Console.ReadLine(), out NumberOfGames);
            if (result)
            {
                return NumberOfGames;
            }
            else
            {
                Console.WriteLine("Мдэ, тебя просили цыфру, а ты...");
                return 1;               
            }
            Console.Clear();           
        }
    }
}
