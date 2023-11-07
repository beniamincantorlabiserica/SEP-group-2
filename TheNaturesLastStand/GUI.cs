namespace TheNaturesLastStand {
    public class GUI {
        public GUI() {

        }

        public void Start(string name, string description) {
            Console.WriteLine("Hello this is just the first stage of The Nature's Last Stand game.");
            Console.WriteLine("You start on an island and you are located on " + name + ", " + description);
            Console.WriteLine("Your goal is to gain as much points as you can in order to save the planet");
        }

        public string ReadCommand() {
            string input;
            input = Console.ReadLine();
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

        public void FinishedGame(int score) {
            Console.WriteLine("Congrats! You finished the game and gained " + score + " points!");
        }
    }
}