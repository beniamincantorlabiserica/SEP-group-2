using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

namespace TheNaturesLastStand
{
    public class Game
    {
        private Player Player;
        private ScreenManager ScreenManager;
        
        /// <summary>
        /// Constructor for running the game, creating a new ScreenManager and a new Player
        /// </summary>
        public Game()
        {
            ScreenManager = new ScreenManager();
            Player = new Player(ScreenManager);
            Run();
        }

        /// <summary>
        /// Function running the game and play the music
        /// </summary>
        public void Run() {

            Player.Init();

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
            
            while(true)
            {
                string Command = Player.ScreenManager.ReadCommand();
                Player.DoCommand(Command.ToLower());
                if(Command.ToLower() == "quit" || Player.HasCompletedGame == true) break;
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string audioFilePath = @"../../../audio.mp3";

            try
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    using (var process = new Process())
                    {
                        process.StartInfo.FileName = "afplay";
                        process.StartInfo.Arguments = audioFilePath;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.CreateNoWindow = true;
                        process.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing file: {ex.Message}");
            }
        }
    }
}
