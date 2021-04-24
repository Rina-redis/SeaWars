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
        public static Dictionary<string, PlayerProfile> allPlayersProfiles = new();
        public void Start()
        {
          allPlayersProfiles = serializer.Deserialize();

            ShowPlayers(allPlayersProfiles);
            Game game = new Game();
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



        public void ShowPlayers(Dictionary<string, PlayerProfile> pairs)
        {
            var values = pairs.Values;
            foreach(PlayerProfile profile in values)
            {
                if (profile != null)
                {                 
                    Console.WriteLine(profile.name);
                    Console.WriteLine(profile.score);                    
                }
            }
          
        }
        //public void ChooseProfile()
        //{
        //    int number;
        //    Console.WriteLine("If you alredy have a player profile wrie 1");
        //    Console.WriteLine("To createa a new profile write 2");
        //    bool result = int.TryParse(Console.ReadLine(), out number);
        //    switch (number)
        //    { 
        //        case 1:ShowPlayers(allPlayersProfiles);

        //                return;

        //        case 2: return;
        //    }

        //}
    }
}
