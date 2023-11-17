namespace TheNaturesLastStand
{
    public class Game
    {
        public int score;
        private Screen Screen;
        private Command command;
        private Seaside seasideBiome;
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

        private Seaside CreateSeaside() 
        {
            List<Quest> allQuests = new List<Quest>{};
            allQuests.Add(new Quest("Pick up plastic", "The plastic is not doing good for the nature"));

            List<Location> locations = new List<Location>{};
            Stack<Item> Items = new Stack<Item>();
            Items.Push(new Item_Lasting(1, "Cool Stick", "A cool stick"));
            locations.Add(new Location("Test Location", "This is just a test description", 1, allQuests, Items));
            Seaside seasideBiome = new Seaside("Seaside", "Seaside biome... the place where the water meets the land...", locations, 400);
            return seasideBiome;

        }

        private void Run() {
            bool playing = true;

            Screen.Display_Inventory_Contents(Player_Inventory);
            Screen.Update();
            Command.Help(Screen);

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
