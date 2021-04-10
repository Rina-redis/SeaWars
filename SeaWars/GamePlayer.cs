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
        //  public string playerName;
        public Field gameField;

        public GamePlayer(PlayerType PlayerType, Field Field)
        {
            playerType = PlayerType;
            gameField = Field;
        }
    }
    enum PlayerType
    {
        bot, human
    }
    class PlayerProfile
    {
        public string name;
        public int score;

        public PlayerProfile(string Name)
        {
            name = Name;
        }
    }
}
