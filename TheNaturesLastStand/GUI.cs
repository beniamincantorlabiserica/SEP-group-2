namespace TheNaturesLastStand {
    public class GUI {
        public GUI() {

        }

        public void Start(string name, string description) {
            Console.WriteLine("Hello this is just the first stage of The Nature's Last Stand game.");
            Console.WriteLine("You start on an island and you are lcoateed on " + name + " ywfgwufwfhwhufw4ufhw " + description);
            Console.WriteLine("Your goal is to gain as much points as you can in order to save the planet");
        }

        public string ReadCommand() {
            
            string input;
            // do {
                Console.WriteLine("DEBUG: reading command...");
                input = Console.ReadLine();
            // } while (!string.IsNullOrEmpty(input));
            Console.WriteLine("DEBUG: current input - " + input);
            return input;
        }

        public void DisplayMessage(string message) {
            Console.WriteLine(message);
        }

        public void HelpMenu() {
            Console.WriteLine("Use this commands to play:");
            Console.WriteLine("- Help: activate help menu");
            Console.WriteLine("- Move: move around the game");
            Console.WriteLine("- Pick: used to complete quests");
            Console.WriteLine("- Quit: quit the game");
        }

        public void GameOver() {
            Console.WriteLine("Thank you for playing");
        }

        public void DisplayInvalidCommand() {
            Console.WriteLine("Invalid command");
        }
    }
}