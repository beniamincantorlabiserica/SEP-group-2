using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNaturesLastStand
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "up", "right", "down", "left", "look", "back", "quit" };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
