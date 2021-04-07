using System;
using System.Diagnostics;

namespace SeaWars
{
     class Program
    {
        //i = y, j = x      
        #region
        static char ShipSymbol = '!';
        static char DiedShipSymbol = '#';
        static char LoseShoot = '^';
        static char EmptyCell = '.';
        static bool ContinueGame = true;
        #endregion symbols

        private static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            ShowSettings();
            GameCycle();
            sw.Stop();
            Console.WriteLine("Ms =" + sw.Elapsed);
        }

        // Game
        public static void GameCycle()
        {
            FieldParams _fieldParams = GetFieldParams();
            Field _playerField = CreateField(_fieldParams);
            Field _botField = CreateField(_fieldParams);
            Console.Clear();

            while (CanPlay())
            {
                DrawField(_playerField);
                //   _botField.DrawField();
                DrawHiddenField(_botField);

                Shoot(ref _botField, false);
                Shoot(ref _playerField, true);

                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            CheckAndCongratulateWinner(_botField);
            AskForNewGame();
        }
        public static void ShowSettings()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Hello, nice too see you hear");
            Console.WriteLine("Short info for you");
            Console.WriteLine("Ship symbol is - " + ShipSymbol);
            Console.WriteLine("Symbol when a ship is hitted is - " + DiedShipSymbol);
            Console.WriteLine("Symbol when a missed shoot - " + DiedShipSymbol);
            Console.WriteLine("Enter anything");
            Console.ReadLine();
            Console.ResetColor();
            Console.Clear();

        }
        public static void AskForNewGame()
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
                    ContinueGame = true;
                    GameCycle();
                    break;
                default:
                    Console.Write("Thanks for playing))");
                    break;
            }
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
        public static bool CanSetShip(char[,] field, int shipPosY, int shipPosX)
        {
            if (field[shipPosY, shipPosX] != ShipSymbol && field[shipPosY, shipPosX - 1] != ShipSymbol &&
                field[shipPosY-1, shipPosX - 1] != ShipSymbol && field[shipPosY-1, shipPosX] != ShipSymbol)     
                return true;
            return false;
                
        }
        public static void StopGame()
        {
            ContinueGame = false;
        }
        public static void Shoot(ref Field fieldToShoot, bool isBot)
        {
            int x;
            int y;
         
            if (!isBot)
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
                fieldToShoot.fieldSymbols[y, x] = DiedShipSymbol;
                fieldToShoot.myfieldParams.ships--;
                if (fieldToShoot.myfieldParams.ships == 0)
                {
                    StopGame();
                }
            }
            else
            {
                Console.WriteLine("Miss..");
                fieldToShoot.fieldSymbols[y, x] = LoseShoot;
            }
        }
        public static bool IsHit(int coordinateY, int coordinateX, Field fieldToShoot)
        {
            if(fieldToShoot.fieldSymbols[coordinateY, coordinateX] == ShipSymbol)
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
            Console.WriteLine("Wait,enemy is attacking you!");           
            System.Threading.Thread.Sleep(1500);
            Random rand = new Random();        
            int coordinateY = rand.Next(1, fieldToShoot.myfieldParams.height);           
            int coordinateX = rand.Next(1, fieldToShoot.myfieldParams.width);
            return (coordinateY, coordinateX);

        }
        public static void DrawField(Field FieldToDraw)
        {
            char[,] fieldSymbols = FieldToDraw.fieldSymbols;
            FieldParams myfieldParams = FieldToDraw.myfieldParams;
            for (int i = 0; i < myfieldParams.height; i++)
            {
                for (int j = 0; j < myfieldParams.width; j++)
                    Console.Write(fieldSymbols[i, j]);
                Console.WriteLine();
            }

        }
         public static void DrawHiddenField(Field FieldToDraw)
        {
            char[,] fieldSymbols = FieldToDraw.fieldSymbols;
            FieldParams myfieldParams = FieldToDraw.myfieldParams;
            for (int i = 0; i < myfieldParams.height; i++)
            {
                for (int j = 0; j < myfieldParams.width; j++)
                {
                    if (fieldSymbols[i, j] == ShipSymbol)
                    {
                        Console.Write(EmptyCell);
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
        public struct Field
        {
            public FieldParams myfieldParams;
            public char[,] fieldSymbols;

            public Field(FieldParams fieldParams, char[,] newFielSymbols)
            {
                myfieldParams = fieldParams;
                fieldSymbols = newFielSymbols;
            }
                
        }
        public struct FieldParams
        {
            public int width;
            public int height;
            public int ships;
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
                    field[i, j] = EmptyCell;

                    if (i == 0 && j == 0)
                    {
                        field[i, j] = EmptyCell;
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
                    field[ShipPosY, ShipPosX] = ShipSymbol;
                    setedShips++;
                }
            }

            Field warField = new Field(fieldParams, field);
            return warField;
        }
    }
}