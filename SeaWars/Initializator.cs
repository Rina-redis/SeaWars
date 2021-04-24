using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWars
{
    class Initializator
    {
        UI uiRef = new UI();

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

        public void TryToGetNameAndCreatePlayerProfile(GamePlayer playerToCheck)
        {
            if (playerToCheck.playerType == PlayerType.human)
            {
                Console.WriteLine("Enter your name:");
                string name = Console.ReadLine();
                playerToCheck.playerProfile = new PlayerProfile(name, Lobby.allPlayersProfiles);
            }
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


     }
    }
