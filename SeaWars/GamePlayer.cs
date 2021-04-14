using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWars
{
    class GamePlayer
    {
        
        public PlayerType playerType;
        public PlayerProfile playerProfile;
        public Field gameField;

        public GamePlayer(PlayerType PlayerType, Field Field, List<GamePlayer> gamePlayers)
        {
            playerType = PlayerType;
            gameField = Field;
            gamePlayers.Add(this);
        }

         public void Shoot(int coordinateX, int coordinateY, Field fieldToShoot)
        {          

            if (IsHit(coordinateY, coordinateX, fieldToShoot))
            {
                Console.WriteLine("It is HIT"); 
                fieldToShoot.fieldSymbols[coordinateY, coordinateX] = Constants.DiedShipSymbol;
                fieldToShoot.myfieldParams.ships--;  
            }
            else
            {
                Console.WriteLine("Miss..");
                fieldToShoot.fieldSymbols[coordinateY, coordinateX] = Constants.LoseShoot;
            }
        }
        public bool IsHit(int coordinateY, int coordinateX, Field fieldToShoot)
        {
            if (fieldToShoot.fieldSymbols[coordinateY, coordinateX] == Constants.ShipSymbol)
            {
                return true;
            }
            else
            {
                return false;
            }        
        }
    }
    enum PlayerType
    {
        bot, human
    }

    class PlayerProfile  //may be a struct?
    {
        public string name;
        public int score = 0;

        public PlayerProfile(string Name) 
        {
            name = Name;
        }

        public void AddScoreForWinner()
        {
            score++;
        }
    }
}
