namespace TheNaturesLastStand
{
    public class Command
    {
       private List<string> commands = new List<string>{};
        public Command() 
        {
            commands.Add("help");
            commands.Add("move");
            commands.Add("quit");
            commands.Add("pick");
        }

        public bool VerifyCommand(string input) {
            foreach (var command in commands)
            {
                if(input == command){
                    return true;
                } 
            }
            return false;
        }

    }
}
