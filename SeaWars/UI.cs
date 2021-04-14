﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWars
{
    class UI
    {
        public PlayerType GetPlayerType()
        {
            string _playerType;
            do
            {
                Console.WriteLine("Enter a type of player (human or bot)");
                _playerType = Console.ReadLine();
                switch (_playerType)
                {
                    case "human":
                        return PlayerType.human;
                    case "bot":
                        return PlayerType.bot;
                }
            }
            while (_playerType != PlayerType.bot.ToString() && _playerType != PlayerType.human.ToString());

            return PlayerType.human;
        }

        public FieldParams GetFieldParams()
        {
            Console.WriteLine("Enter Height of Field");
            int height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Width of Field");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter a number of Ships");
            int ships = Convert.ToInt32(Console.ReadLine());

            FieldParams newParams = new FieldParams();
            newParams.width = width + 1;
            newParams.height = height + 1;
            newParams.ships = ships;

            return newParams;
        }
        public bool WantToUsePreset()
        {
            Console.WriteLine("Want to use a preset?");
            string wantToUsePreset = Console.ReadLine();
            if (wantToUsePreset == "yes")
                return true;
            else
                return false;
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
                    Lobby lobby = new Lobby();
                    lobby.Start();
                    break;
                default:
                    Console.Write("Thanks for playing))");
                    break;
            }
        }
        public  void ShowSettings()
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
        public void DrawField(GamePlayer gamePlayer)
        {
            if (gamePlayer.playerType == PlayerType.human)
            {
                DrawOpenField(gamePlayer);   //dont work with 2 humans             
            }
            else
            {
                DrawHiddenField(gamePlayer);
            }
        }
        public void DrawOpenField(GamePlayer botGamePlayer)
        {
            char[,] fieldSymbols = botGamePlayer.gameField.fieldSymbols;
            FieldParams myfieldParams = botGamePlayer.gameField.myfieldParams;
            for (int i = 0; i < myfieldParams.height; i++)
            {
                for (int j = 0; j < myfieldParams.width; j++)
                    Console.Write(fieldSymbols[i, j]);
                Console.WriteLine();
            }

        }
        public void DrawHiddenField(GamePlayer botGamePlayer)
        {
            char[,] fieldSymbols = botGamePlayer.gameField.fieldSymbols;
            FieldParams myfieldParams = botGamePlayer.gameField.myfieldParams;
            for (int i = 0; i < myfieldParams.height; i++)
            {
                for (int j = 0; j < myfieldParams.width; j++)
                {
                    if (fieldSymbols[i, j] == Constants.ShipSymbol)
                    {
                        Console.Write(Constants.EmptyCell);
                    }
                    else
                    {
                        Console.Write(fieldSymbols[i, j]);
                    }

                }
                Console.WriteLine();
            }

        }
        public void CongratulateWinner(GamePlayer winner)
        {
            if (winner.playerType != PlayerType.bot)
            {
                Console.WriteLine("Score of " + winner.playerProfile.name + "is  " + winner.playerProfile.score);
                Console.WriteLine("Congtatulate to " + winner.playerProfile.name);
            }
            else
                Console.WriteLine("Bot wins");
        }
       
    }
}
