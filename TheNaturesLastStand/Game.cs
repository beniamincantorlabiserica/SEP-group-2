namespace TheNaturesLastStand
{
    public class Game
    {
        private Player Player;
        private ScreenManager ScreenManager;
        public Game()
        {
            Player = new Player(ScreenManager);
            Run();
        }

        public void Run() {

            Player.Init();
            
            while(true) {
                string Command = Console.ReadLine() ?? string.Empty;
                Player.DoCommand(Command.ToLower());
                if(Command.ToLower() == "quit") break;
            }
        }
    }
}
