namespace TheNaturesLastStand
{
    public class Game
    {
        public int score;
        private Screen Screen;
        private Command command;
        private Seaside seasideBiome;

        public Game()
        {
            score = 0;
            
            init();
            Run();
        }

        private void init() {
            seasideBiome = CreateSeaside();
            Screen = new Screen();
            command = new Command();
        }

        private Seaside CreateSeaside() {
            List<Quest> allQuests = new List<Quest>{};
            allQuests.Add(new Quest("Pick up plastic", "The plastic is not doing good for the nature"));

            List<Location> locations = new List<Location>{};
            locations.Add(new Location("Test Location", "This is just a test description", 1, allQuests));
            Seaside seasideBiome = new Seaside("Seaside", "Seaside biome... the place where the water meets the land...", locations, 400);
            return seasideBiome;

        }

        private void Run() {
            bool playing = true;

            Screen.Start();

            while (playing) {
                string userInput = Screen.ReadCommand();
                if(command.VerifyCommand(userInput)) 
                {
                    switch (userInput.ToLower().Trim())
                    {
                        case "move":
                            Screen.Write_To_Command_Window("You are in " + seasideBiome.locations[0].name + " biome.");
                            Screen.Write_To_Command_Window("New Quest Available!");
                            Screen.Write_To_Command_Window(seasideBiome.locations[0].quests[0].name + " " +seasideBiome.locations[0].quests[0].description); 
                            break;
                        case "pick":
                            seasideBiome.locations[0].quests[0].done = true;
                            Screen.Write_To_Command_Window("Congrats you finished the quest! +200 points");
                            score += 200;
                            Screen.Draw_Box("Score: " + score.ToString());
                            playing = false;
                            break;
                        case "quit": 
                            playing = false;
                            break;
                        case "help": 
                            //gui.HelpMenu();
                            break;
                        default:
                            break;
                    }
                } else {
                    Screen.Write_To_Command_Window("Invalid command");
                }
            }
            Screen.GameOver();
            Screen.Update();
        }
    }
}
