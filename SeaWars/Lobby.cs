using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWars
{
   
    class Lobby
    {
        Game game = new Game();              
        public void Start()
        {
            ShowSettings();
            game.Start(NumberOfGamesToPlay());
            AskForNewGame();
        }

        public void AskForNewGame()
        {
            Console.WriteLine("Want to play Again?");
            Console.WriteLine("If Yes, write 1, If No write 0");
            int playAgain = Convert.ToInt32(Console.ReadLine());
            switch (playAgain)
            {
                case 0:
                    Console.Write("Thanks for playing))");
                    break;
                case 1:
                    Start();
                    break;
                default:
                    Console.Write("Thanks for playing))");
                    break;
            }
        }
        public static void ShowSettings()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Hello, nice too see you hear");
            Console.WriteLine("Short info for you");
            Console.WriteLine("Ship symbol is - " + Constants.ShipSymbol);
            Console.WriteLine("Symbol when a ship is hitted is - " + Constants.DiedShipSymbol);
            Console.WriteLine("Symbol when a missed shoot - " + Constants.LoseShoot);
            Console.WriteLine("Enter anything");
            Console.ReadLine();
            Console.ResetColor();
            Console.Clear();

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
