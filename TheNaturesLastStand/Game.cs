using System.Reflection.Metadata;

namespace TheNaturesLastStand
{
    public class Game
    {
        private int score;
        private Command command;
        private GUI interface;

        public Game()
        {
            score = 0;
            CreateSeaside();

            init();
            Run();
        }

        private void CreateSeaside() {
            Quest quest = new Quest("Pick up plastic", "The plastic is not doing good for the nature");
            Quest[] allQuests = new Quest[];
            allQuests[0] = quest;
            Location locationRight = new Location(allQuests, 1, "right");
            Location[] allLocations = new Location[];
            allLocations[0] = locationRight;
            Seaside seasideBiome = new IBiome("Seaside", "Seaside biome... the place where the water meets the land...", allLocations, 400); // add scoreNeededToPass to constructor
        }

        private void init() {
            command = new Command();
            interface = new Guid();
        }

        private void Run() {
            bool playing = true;

            interface.Start();
            
            while(playing) {
                string userInput = interface.ReadCommand(); // change type of return from Command to string
                if(Command.VerifyCommand(userInput)) {
                    switch (userInput)
                    {
                        case "Move": 
                            interface.DisplayMessage("You are in " + seasideBiome.name + " biome.\n");
                            interface.DisplayMessage("New Quest Available!");
                            interface.DisplayMessage(); 
                        case "Pick":
                            
                        case "Quit": 
                            playing = false;
                            break;
                        default:
                    }
                } else {
                    interface.DisplayInvalidCommand(); // funtion to add to GUI
                }

            }
        }
    }
}
