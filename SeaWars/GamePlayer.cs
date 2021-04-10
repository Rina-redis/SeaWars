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
    }
    enum PlayerType
    {
        bot, human
    }
    class PlayerProfile
    {
        public string name;
        public int score = 0;

        public PlayerProfile(string Name) //по приколу тут весит
        {
            name = Name;
        }
    }
}
