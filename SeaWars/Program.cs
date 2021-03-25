using System;

namespace SeaWars
{
     class Program
    {
        static char ShipSymbol = '!';
        private static void Main(string[] args)
        {
            //Запуск игры
            FieldParams _fieldParams = GetFieldParams();
            Field _playerField = CreateField(_fieldParams);
            Field _botField = CreateField(_fieldParams);
            _playerField.DrawField();
            _botField.DrawField();
          //  _playerField.DrawBackField();
            Shoot(_botField, false);
            Shoot(_playerField, true);
        


            //Взятие данных поля
            //Создание поля
            //Отрисовка поля
            //Отрисовка пустого поля где будет выстрел
            //Создание Поля бота

            //Ход игрока
            //Изменение 2-го поля

            //Ход ИИ
            //Изменеие поля игрока

            //(Выполняеться пока у игрока и бота есть не убитые корабли)

            // Остановка игр если у кого-то нет кораблей
        }

        public static FieldParams GetFieldParams()
        {
            int Wight = Convert.ToInt32(Console.ReadLine());
            int Height = Convert.ToInt32(Console.ReadLine());
            int Ships = Convert.ToInt32(Console.ReadLine());

            FieldParams newParams = new FieldParams();
            newParams.wight = Wight+1;
            newParams.height = Height+1;
            newParams.ships = Ships;

            return newParams;
        }

        public static Field CreateField(FieldParams fieldParams)
        {
            char[,] field = new char[fieldParams.wight, fieldParams.height];
            int _height = fieldParams.height;
            int _width = fieldParams.wight;
            int _ships = fieldParams.ships;

            Random rand = new Random();
            char SymbolToPrint = '.';
            

            int _gridNumber = 49;
            int _gridLetter = 65;


            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    field[i, j] = SymbolToPrint;

                    if (i==0 && j == 0)
                    {
                        field[i, j] = SymbolToPrint;
                    }
                    if (i == 0 && j!=0)
                    {
                       
                        field[i, j] = (char)_gridLetter;
                        _gridLetter++;
                    }
                    if (i != 0 && j == 0)
                    {
                        field[i, j] = (char)_gridNumber;
                        _gridNumber++;
                    }                   
                   
                }                 
            }

            int _setedShips = 0;

                while (_setedShips< _ships)
            {
                int ShipPosY = rand.Next(1, _height);
                int ShipPosX = rand.Next(1, _height);
                if (field[ShipPosY, ShipPosX] != ShipSymbol)
                {
                    field[ShipPosY, ShipPosX] = ShipSymbol;
                    _setedShips++;
                }          
            }
                                  
            Field _warField = new Field(fieldParams, field);
            return _warField;
        }

        public static void Shoot(Field FieldToShoot, bool IsBot)
        {
            int x;
            int y;
            Console.WriteLine(FieldToShoot.fieldSymbols.Length);
            if (!IsBot)
            {
                (y, x) = GetShootCoordinates();
            }
            else
            {
                (y, x) = GetShootCoordinatesForBot(FieldToShoot);
            }
            //Console.WriteLine(botField.fieldSymbols[y, x] + " "+ botField.fieldSymbols[x, y] + " " + botField.fieldSymbols[x-1, y-1] + " " + botField.fieldSymbols[x - 1, y] + " " + botField.fieldSymbols[x, y - 1]);
            if (IsHit(y, x, FieldToShoot))
            {
                Console.WriteLine("kchau");
            }
            else
            {
                Console.WriteLine("shit");
            }
        }
        public static bool IsHit(int CoordinateY, int CoordinateX, Field botField)
        {
            if(botField.fieldSymbols[CoordinateY, CoordinateX] == ShipSymbol)
            {
                return true;
            }
            return false;
        }

        public static (int,int) GetShootCoordinates()
        {
            int CoordinateY = Convert.ToInt32(Console.ReadLine());
            char TempCoordinateX = Convert.ToChar(Console.ReadLine());
            int CoordinateX = (int)TempCoordinateX - 64;
            return (CoordinateY, CoordinateX);

        }
        public static (int, int) GetShootCoordinatesForBot( Field FieldToShoot)
        {
           // UI.WriteASentence(ConsoleColor.Cyan, "Wait,enemy is attacking you!");
            System.Threading.Thread.Sleep(1500);
            Random rand = new Random();        
            int CoordinateY = rand.Next(1, 10);  // надо 10 заменить на длинну и высоту массива          
            int CoordinateX = rand.Next(1, 10);
            return (CoordinateY, CoordinateX);

        }
        public struct Field
        {
            public FieldParams myfieldParams;
            public char[,] fieldSymbols;

            public Field(FieldParams fieldParams, char[,] newFielSymbols)
            {
                myfieldParams = fieldParams;
                fieldSymbols = newFielSymbols;
            }

            public void DrawField()
            {
                for (int i = 0; i < myfieldParams.height; i++)
                {
                    for (int j = 0; j < myfieldParams.wight; j++)
                        Console.Write(fieldSymbols[i, j]);
                    Console.WriteLine();
                }
            }

            public void DrawBackField()
            {
                for (int i = 0; i < myfieldParams.height; i++)
                {
                    for (int j = 0; j < myfieldParams.wight; j++)

                        Console.Write(fieldSymbols[i, j]);

                    Console.WriteLine();
                }
            }

        }

        public struct FieldParams
        {
            public int wight;
            public int height;
            public int ships;
        }
    }
}