using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SeaWars
{
    class Game
    {
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
            CreatePlayersAndFields();

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

        public void CreatePlayersAndFields()
        {
            if (uiRef.WantToUsePreset())
            {
                UsePreset();
            }
            else
            {
                DontUsePreset();
            }
        }

        private void DontUsePreset()
        {
            FieldParams _fieldParams = uiRef.CreateFieldParams();

            _player1 = new GamePlayer(uiRef.GetPlayerType(), CreateField(_fieldParams));
            TryToGetNameAndCreatePlayerProfile(_player1);

            _player2 = new GamePlayer(uiRef.GetPlayerType(), CreateField(_fieldParams));
            TryToGetNameAndCreatePlayerProfile(_player2);
            Console.Clear();
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

        public bool CanSetShip(char[,] field, int shipPosY, int shipPosX)
        {
            if (field[shipPosY, shipPosX] != Constants.ShipSymbol &&
                field[shipPosY, shipPosX - 1] != Constants.ShipSymbol &&
                field[shipPosY - 1, shipPosX - 1] != Constants.ShipSymbol &&
                field[shipPosY - 1, shipPosX] != Constants.ShipSymbol)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Field CreateField(FieldParams fieldParams)
        {
            char[,] field = new char[fieldParams.height, fieldParams.width];
            int height = fieldParams.height;
            int width = fieldParams.width;
            int ships = fieldParams.ships;

            Random rand = new Random();

            int gridNumber = 49;
            int gridLetter = 65;

            for (int i = 0; i < height; i++) //почему-то поле может быть только квадратным
            {
                for (int j = 0; j < width; j++)
                {
                    field[i, j] = Constants.EmptyCell;

                    if (i == 0 && j == 0)
                    {
                        field[i, j] = Constants.EmptyCell;
                    }

                    if (i == 0 && j != 0)
                    {
                        field[i, j] = (char) gridLetter;
                        gridLetter++;
                    }

                    if (i != 0 && j == 0)
                    {
                        field[i, j] = (char) gridNumber;
                        gridNumber++;
                    }
                }
            }

            int setedShips = 0;
            while (setedShips < ships)
            {
                int ShipPosY = rand.Next(1, height);
                int ShipPosX = rand.Next(1, height);
                if (CanSetShip(field, ShipPosY, ShipPosX))
                {
                    field[ShipPosY, ShipPosX] = Constants.ShipSymbol;
                    setedShips++;
                }
            }

            Field warField = new Field(fieldParams, field);
            return warField;
        }

        public void TryToGetNameAndCreatePlayerProfile(GamePlayer playerToCheck)
        {
            if (playerToCheck.playerType == PlayerType.human)
            {
                Console.WriteLine("Enter your name:");
                string name = Console.ReadLine();
                playerToCheck.playerProfile = new PlayerProfile(name, Lobby.allPlayersProfiles);
            }
        }

        public void UsePreset()
        {
            int presetNumber;
            Console.WriteLine("Enter a number of preset");
            bool result = int.TryParse(Console.ReadLine(), out presetNumber);
            switch (presetNumber)
            {
                case 1:
                    FieldParams fieldParams;
                    fieldParams.height = 9;
                    fieldParams.width = 9;
                    fieldParams.ships = 5;
                    _player1 = new GamePlayer(PlayerType.human, CreateField(fieldParams));
                    TryToGetNameAndCreatePlayerProfile(_player1);
                    _player2 = new GamePlayer(PlayerType.bot, CreateField(fieldParams));
                    break;
            }
        }
    }
}