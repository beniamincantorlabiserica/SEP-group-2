namespace TheNaturesLastStand
{
    public class Game
    {
        public int score;
        private Screen Screen;
        private Command command;
        private Biome seasideBiome;
        private Inventory Player_Inventory;

        public Game()
        {
            score = 0;
            
            init();
            Run();
        }

        private void init() 
        {
            seasideBiome = CreateSeaside();
            Screen = new Screen();
            Screen.Start();
            command = new Command();
            Player_Inventory = new Inventory(15);
            Player_Inventory.Add(new Item_Lasting(2, "Garbage", "A piece of garbage that you foind on the seashore"));
        }

        private Biome CreateSeaside() 
        {
            List<Location> Locations = new List<Location>() { new Location("Start", new Quest("Starter Quest", "Quest", "Good", "Bad"), null)};
            Locations.Add(new Location("Upper Location",null,null));
            Locations.Add(new Location("Lower Location", null, null));
            Biome Seaside_Biome = new Biome("Seaside", "This is the Seaside Biome", Locations, 500);

            return Seaside_Biome;
        }

        private void Run() {
            bool playing = true;
            int Current_Location_Index = 0;

            Screen.Display_Inventory_Contents(Player_Inventory);
            Screen.Update();
            Command.Help(Screen);

            while (playing) {
                string userInput = Screen.ReadCommand().ToLower().Trim();
                if(command.VerifyCommand(userInput)) 
                {
                    switch (userInput)
                    {
                        case "move":
                            Current_Location_Index = Command.Move_To(Screen, seasideBiome.locations.ToArray(), Current_Location_Index, userInput);
                            Screen.Write_To_Command_Window("You are in " + seasideBiome.locations[0].name + " biome.");
                            Screen.Write_To_Command_Window("New Quest Available!");
                            Screen.Write_To_Command_Window(seasideBiome.locations[0].quest.name + " " +seasideBiome.locations[0].quest.description); 
                            break;
                        case "gather":
                            Command.Gather(Screen, ref Player_Inventory, seasideBiome.locations[0]);
                            break;
                        case "quit": 
                            playing = false;
                            break;
                        case "help":
                            Command.Help(Screen);
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
