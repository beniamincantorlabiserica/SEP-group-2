namespace TheNaturesLastStand
{
    public class Game
    {
        private Player Player;
        private ScreenManager ScreenManager;
        public Game()
        {
            ScreenManager = new ScreenManager();
            Player = new Player(ScreenManager);
            Run();
        }

        public void Run() {

            Player.Init();
            
            while(true)
            {
                string Command = Player.ScreenManager.ReadCommand();
                Player.DoCommand(Command.ToLower());
                if(Command.ToLower() == "quit") break;
            }
        }
    }
}
