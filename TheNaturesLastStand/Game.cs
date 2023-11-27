namespace TheNaturesLastStand
{
    public class Game
    {
        public Game() {
            Run();
        }

        public void Run() {

            Player.init();
            
            while(true) {
                string command = Console.ReadLine();
                if(command.toLower() == "quit") break;
                Player.DoCommand(command);
            }
        }
    }
}
