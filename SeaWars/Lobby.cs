using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWars
{   
    class Lobby
    {
        private UI uiRef = new UI();
        static Serializer serializer = new Serializer();
        Initializator initializator = new Initializator();

        Dictionary<string, PlayerProfile> allPlayersProfilesDictionary = new();
        public static List<PlayerProfile> allPlayersProfiles = new();
        private GamePlayer _player1;
        private GamePlayer _player2;

        public void Start()
        {
           allPlayersProfiles = serializer.Deserialize();

            ShowPlayers(allPlayersProfiles);
            InitializePlayers(ref _player1, ref _player2);
            Game game = new Game(_player1, _player2);
            uiRef.ShowSettings();
            game.Start(NumberOfGamesToPlay());

            serializer.Serialize(allPlayersProfiles);
            uiRef.AskForNewGame();
        }
        public int NumberOfGamesToPlay()
        {
            int NumberOfGames;
            Console.WriteLine("Enter a number of games which you want to play");
             bool result = int.TryParse(Console.ReadLine(), out NumberOfGames);
            if (result)
            {
                return NumberOfGames;
            }
            else
            {
                Console.WriteLine("Мдэ, тебя просили цыфру, а ты...");
                return 1;               
            }
            Console.Clear();           
        }

        public void ShowPlayers(List<PlayerProfile> playerProfiles)
        {
           foreach( PlayerProfile profile in playerProfiles)
            {
                Console.WriteLine(profile.name);
                Console.WriteLine(profile.score);
            }        
        }
        public void ChooseProfile()
        {
            int number;
            Console.WriteLine("If you alredy have a player profile wrie 1");
            Console.WriteLine("To createa a new profile write 2");
            bool result = int.TryParse(Console.ReadLine(), out number);
            switch (number)
            {
                case 1:
                    ShowPlayers(allPlayersProfiles);
 
                    return;
                case 2: return;
            }
        }
     
     
        public void InitializePlayers(ref GamePlayer player1, ref GamePlayer player2 )
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
                    _player1 = new GamePlayer(PlayerType.human, initializator.CreateField(fieldParams));
                    initializator.TryToGetNameAndCreatePlayerProfile(_player1);
                    _player2 = new GamePlayer(PlayerType.bot, initializator.CreateField(fieldParams));
                    break;
            }
        }
        public void DontUsePreset()
        {
            FieldParams _fieldParams = uiRef.CreateFieldParams();

            _player1 = new GamePlayer(uiRef.GetPlayerType(), initializator.CreateField(_fieldParams));
            initializator.TryToGetNameAndCreatePlayerProfile(_player1);

            _player2 = new GamePlayer(uiRef.GetPlayerType(), initializator.CreateField(_fieldParams));
            initializator.TryToGetNameAndCreatePlayerProfile(_player2);
            Console.Clear();
        }

    }
}
