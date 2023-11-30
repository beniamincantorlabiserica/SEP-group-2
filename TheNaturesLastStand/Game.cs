using System.Diagnostics;

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
            
            
            // string audioFilePath = @"../../../song.wav";
            //
            // try
            // {
            //     using (var process = new Process())
            //     {
            //         process.StartInfo.FileName = "afplay";
            //         process.StartInfo.Arguments = audioFilePath;
            //         process.StartInfo.UseShellExecute = false;
            //         process.StartInfo.CreateNoWindow = true;
            //         process.Start();
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Error playing file: {ex.Message}");
            // }
            
            while(true)
            {
                string Command = Player.ScreenManager.ReadCommand();
                Player.DoCommand(Command.ToLower());
                if(Command.ToLower() == "quit") break;
            }
        }
    }
}
