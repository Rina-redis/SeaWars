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

        public GamePlayer(PlayerType PlayerType, Field Field)
        {
            playerType = PlayerType;
            gameField = Field;
           
        }

         public void Shoot(int coordinateX, int coordinateY, ref Field fieldToShoot)
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
          => fieldToShoot.fieldSymbols[coordinateY, coordinateX] == Constants.ShipSymbol;
    }
    enum PlayerType
    {
        bot, human
    }

    [Serializable]
    public class PlayerProfile  //may be a struct?
    {
        public string name;
        public int score = 0;

        public PlayerProfile(string Name, List<PlayerProfile> profiles) 
        {
            name = Name;
            profiles.Add(this);
        }
        public PlayerProfile()
        {
            
        }
        public void AddScoreForWinner()
        {
            score++;
        }
    }
}
