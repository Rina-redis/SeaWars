using System;
using System.Diagnostics;


namespace SeaWars
{

    class Game
    {

        //i = y, j = x      
        #region
        static bool ContinueGame = true;
        #endregion symbols
        private static GamePlayer _player1;
        private static GamePlayer _player2;
        // Game
        public void Start()
        {           
            CreatePlayersAndFields();                   
            while (CanPlay())
            {
                DrawField(_player1);
                DrawField(_player2);

                Shoot(ref _player2.gameField, _player1);
                Shoot(ref _player1.gameField, _player2);

                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            CheckAndCongratulateWinner(_player2.gameField);
            
        }          
        public static void CheckAndCongratulateWinner(Field botField)
        {
            if (botField.myfieldParams.ships == 0)
            {
                Console.WriteLine("Player Win!!");
            }
            else
            {
                Console.WriteLine("Bot Win!!");
            }
        }
        public static bool CanPlay()
        {
            return ContinueGame;
        }
      
        //Logic, shoots, coordinates
        public static void CreatePlayersAndFields()
        {
            FieldParams _fieldParams = GetFieldParams();

            _player1 = new GamePlayer(GetPlayerType(), CreateField(_fieldParams));
            _player2 = new GamePlayer(GetPlayerType(), CreateField(_fieldParams));
            Console.Clear();

        }
        public static void StopGame()
        {
            ContinueGame = false;
        }
        public static void Shoot(ref Field fieldToShoot, GamePlayer currentPlayer)
        {
            int x;
            int y;
         
            if (currentPlayer.playerType != PlayerType.bot)
            {
                (y, x) = GetShootCoordinates();
            }
            else
            {
                (y, x) = GetShootCoordinatesForBot(fieldToShoot);
            }
            //Console.WriteLine(botField.fieldSymbols[y, x] + " "+ botField.fieldSymbols[x, y] + " " + botField.fieldSymbols[x-1, y-1] + " " + botField.fieldSymbols[x - 1, y] + " " + botField.fieldSymbols[x, y - 1]);
            if (IsHit(y, x, fieldToShoot))
            {
                Console.WriteLine("It is HIT"); // a bit shit
                fieldToShoot.fieldSymbols[y, x] = Constants.DiedShipSymbol;
                fieldToShoot.myfieldParams.ships--;
                if (fieldToShoot.myfieldParams.ships == 0)
                {
                    StopGame();
                }
            }
            else
            {
                Console.WriteLine("Miss..");
                fieldToShoot.fieldSymbols[y, x] = Constants.LoseShoot;
            }
        }
        public static bool IsHit(int coordinateY, int coordinateX, Field fieldToShoot)
        {
            if(fieldToShoot.fieldSymbols[coordinateY, coordinateX] == Constants.ShipSymbol)
            {
                return true;
            }
            return false;
        }
        public static (int,int) GetShootCoordinates()
        {
            
            Console.WriteLine("Enter a number for shoot");
            int CoordinateY = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter a letter for shoot");
            string toUpper = Convert.ToString(Console.ReadLine()).ToUpper();
            char TempCoordinateX = Convert.ToChar(toUpper);        
            int CoordinateX = (int)TempCoordinateX - 64;
            return (CoordinateY, CoordinateX);

        }
        public static (int, int) GetShootCoordinatesForBot(Field fieldToShoot)
        {
            Console.WriteLine("Wait,enemy is attacking!");           
            System.Threading.Thread.Sleep(1500);
            Random rand = new Random();        
            int coordinateY = rand.Next(1, fieldToShoot.myfieldParams.height);           
            int coordinateX = rand.Next(1, fieldToShoot.myfieldParams.width);
            return (coordinateY, coordinateX);

        }
        public static void DrawField(GamePlayer gamePlayer)
        {
            char[,] fieldSymbols = gamePlayer.gameField.fieldSymbols;
            FieldParams myfieldParams = gamePlayer.gameField.myfieldParams;

            if (gamePlayer.playerType == PlayerType.human)
            {
                for (int i = 0; i < myfieldParams.height; i++)
                {
                    for (int j = 0; j < myfieldParams.width; j++)
                        Console.Write(fieldSymbols[i, j]);
                    Console.WriteLine();
                }
            }
            else
                DrawHiddenField(gamePlayer);

        }
         public static void DrawHiddenField(GamePlayer botGamePlayer)
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

        //Fields
        public static Field CreateField(FieldParams fieldParams)
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
                        field[i, j] = (char)gridLetter;
                        gridLetter++;
                    }
                    if (i != 0 && j == 0)
                    {
                        field[i, j] = (char)gridNumber;
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
        public static bool CanSetShip(char[,] field, int shipPosY, int shipPosX)
        {
            if (field[shipPosY, shipPosX] != Constants.ShipSymbol && field[shipPosY, shipPosX - 1] != Constants.ShipSymbol &&
                field[shipPosY - 1, shipPosX - 1] != Constants.ShipSymbol && field[shipPosY - 1, shipPosX] != Constants.ShipSymbol)
                return true;
            return false;

        }
        public static FieldParams GetFieldParams()
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
        public static PlayerType GetPlayerType()
        {
            string _playerType;
            do
            {
                Console.WriteLine("Enter a type of player (human or bot)");
                _playerType = Console.ReadLine();
                switch (_playerType)
                {
                    case "human":
                        Console.WriteLine("Enter your name");
                        string name = Console.ReadLine();
                        PlayerProfile profile = new PlayerProfile(name);
                        return PlayerType.human;

                        break;
                    case "bot":
                        return PlayerType.bot;

                        break;
                }
            }
            while (_playerType != "human" && _playerType != "bot"); //bad construction!!

            return PlayerType.human;
        }

    }
}