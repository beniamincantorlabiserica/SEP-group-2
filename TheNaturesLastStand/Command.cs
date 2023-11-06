namespace TheNaturesLastStand
{
    public class Command
    {
       private List<string> commands = new List<string>{};
        public Command() {
            commands.Add("Help");
            commands.Add("Move");
            commands.Add("Quit");
            commands.Add("Pick");
        }

        public bool VerifyCommand(string input) {
            foreach (var command in commands)
            {
                if(input.Equals(command)) return true;
            }
            return false;
        }

    }
}
