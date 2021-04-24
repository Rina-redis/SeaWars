using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SeaWars
{
    class Game
    {
        public Game(GamePlayer player1, GamePlayer player2)
        {
            _player1 = player1;
            _player2 = player2;
        }
        //i = y, j = x      

        #region

        bool ContinueGame = true;

        #endregion symbols
        private GamePlayer _player1;
        private GamePlayer _player2;
        private GamePlayer winner;
        private UI uiRef = new UI();
    
        // Game
        public void Start(int numberOfGames)
        {
           
            for (int alredyPlayedGames = 0; alredyPlayedGames < numberOfGames; alredyPlayedGames++)
            {
                while (CanPlay())
                {
                    uiRef.DrawOpenField(_player1);
                    uiRef.DrawOpenField(_player2);

                    (int x1, int y1) = GetShootCoordinates(_player1);
                    _player1.Shoot(x1, y1, ref _player2.gameField);

                    (int x2, int y2) = GetShootCoordinates(_player2);
                    _player2.Shoot(x2, y2, ref _player1.gameField);

                    CheckWinner(_player1, _player2);

                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }
                EndGame();
                ContinueGame = true;
            }
        }


        public void EndGame()
        {
            winner.playerProfile.AddScoreForWinner();
            uiRef.CongratulateWinner(winner);
           // uiRef.ShowResultsOfGame(Lobby.allPlayersProfiles.Values);           
        }

        public bool CanPlay()
        {       
            return ContinueGame;
        }

        public void StopGame()
        {
            ContinueGame = false;
        }
   
        public void CheckWinner(GamePlayer player1, GamePlayer player2)
        {
            if (player1.gameField.myfieldParams.ships == 0)
            {
                winner = player2;
                StopGame();
            }

            if (player2.gameField.myfieldParams.ships == 0)
            {
                winner = player1;
                StopGame();
            }
        }

        public (int, int) GetShootCoordinates(GamePlayer player)
        {
            int x;
            int y;
            if (player.playerType == PlayerType.human)
            {
                (x, y) = GetShootCoordinatesForHuman();
            }
            else
            {
                (x, y) = GetShootCoordinatesForBot(player.gameField);
            }

            return (x, y);
        }

        public (int, int) GetShootCoordinatesForBot(Field field)
        {
            Console.WriteLine("Wait,enemy is attacking!");
            System.Threading.Thread.Sleep(1500);
            Random rand = new Random();
            int coordinateY = rand.Next(1, field.myfieldParams.height);
            int coordinateX = rand.Next(1, field.myfieldParams.width);
            return (coordinateY, coordinateX);
        }

        public (int, int) GetShootCoordinatesForHuman()
        {
            Console.WriteLine("Enter a number for shoot");
            int CoordinateY;
            bool result = int.TryParse(Console.ReadLine(), out CoordinateY);
            if (!result)
            {
                GetShootCoordinatesForHuman();
            }

            Console.WriteLine("Enter a letter for shoot");
            string toUpper = Convert.ToString(Console.ReadLine()).ToUpper();
            char TempCoordinateX = Convert.ToChar(toUpper);
            int CoordinateX = (int) TempCoordinateX - 64;
            return (CoordinateX, CoordinateY);
        }

      

       

       
    }
}