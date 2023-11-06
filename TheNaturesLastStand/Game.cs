namespace TheNaturesLastStand
{
    public class Game
    {
        private int score;
        private GUI gui;
        private Command command;
        private Biome seasideBiome;

        public Game()
        {
            score = 0;
            
            init();
            Run();
        }

        private void init() {
            seasideBiome = CreateSeaside();
            gui = new GUI();
            command = new Command();
        }

        private Biome CreateSeaside() {
            List<Quest> allQuests = new List<Quest>{};
            allQuests.Add(new Quest("Pick up plastic", "The plastic is not doing good for the nature"));

            List<Location> locations = new List<Location>{};
            locations.Add(new Location("Test Location", "This is just a test description", 1, allQuests));

            Biome seasideBiome = new Seaside("Seaside", "Seaside biome... the place where the water meets the land...", locations, 400);
            return seasideBiome;

        }

        private void Run() {
            bool playing = true;

            gui.Start(seasideBiome.name, seasideBiome.description);
            
            while(playing) {
                string userInput = gui.ReadCommand(); 
                if(command.VerifyCommand(gui.ReadCommand())) {
                    switch (userInput)
                    {
                        case "Move": 
                            gui.DisplayMessage("You are in " + seasideBiome.locations[0].name + " biome.\n");
                            gui.DisplayMessage("New Quest Available!");
                            gui.DisplayMessage(seasideBiome.locations[0].quests[0].name + "\n" + seasideBiome.locations[0].quests[0].description); 
                            break;
                        case "Pick":
                            seasideBiome.locations[0].quests[0].done = true;
                            gui.DisplayMessage("Congrats you finished the quest! +200 points");
                            score += 200;
                            break;
                        case "Quit": 
                            playing = false;
                            break;
                        default:
                            break;
                    }
                } else {
                    gui.DisplayInvalidCommand(); // funtion to add to GUI
                }
            }
            gui.GameOver();
        }
    }
}
