namespace TheNaturesLastStand
{
    public class Game
    {
        private int score;
        private GUI interface;
        private Biome seasideBiome;

        public Game()
        {
            score = 0;
            
            init();
            Run();
        }

        private void init() {
            seasideBiome = CreateSeaside();
            // interface = new GUI();
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

            interface.Start(seasideBiome.name, seasideBiome.description);
            
            while(playing) {
                string userInput = interface.ReadCommand(); 
                if(Command.VerifyCommand(interface.ReadCommand())) {
                    switch (userInput)
                    {
                        case "Move": 
                            interface.DisplayMessage("You are in " + seasideBiome.locations[0].name + " biome.\n");
                            interface.DisplayMessage("New Quest Available!");
                            interface.DisplayMessage(seasideBiome.locations[0].quests[0].name + "\n" + seasideBiome.locations[0].quests[0].description); 
                        case "Pick":
                            seasideBiome.locations[0].quests[0].done = true;
                            interface.DisplayMessage("Congrats you finished the quest! +200 points");
                            score += 200;
                        case "Quit": 
                            playing = false;
                            break;
                        default:
                    }
                } else {
                    interface.DisplayInvalidCommand(); // funtion to add to GUI
                }
            }
            interface.GameOver();
        }
    }
}
