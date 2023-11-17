namespace TheNaturesLastStand
{
    public class Command
    {
        private string[] Commands = new string[] {"help", "move", "quit", "gather" };
        private string[] Move_Commands = new string[] { "north", "west", "south", "east" };

        public bool VerifyCommand(string Input) 
        {
            string[] Input_Commands = Input.Split(' ');

            if (Commands.Contains(Input_Commands[0]))
            {
                if(Input_Commands.Length == 1)
                {
                    return true;
                }
                else
                {
                    switch (Input_Commands[0])
                    {
                        case "move":
                            if (Move_Commands.Contains(Input_Commands[1]))
                            {
                                return true;
                            }
                            return false;
                    }
                }
            }
            return false;
        }

        public static void Help(Screen Screen)
        {
            string Help_Text = @"Commands:
*help* Show this help menu
*move (direction)* Moves in the specified direction if a location exists
*gather* Gathers items and adds them to the player's inventory
*drop (item name)* Drops the specified item on the ground";
            Screen.Draw_Box(Help_Text);
        }

        public static int Move_To(Screen Screen, Location[] Locations, int Current_Location_Index, string Input)
        {
            //0 == up 1 == right 2 == down 3 == left
            string Second_Argument = Input.Split(' ')[1];

            switch (Second_Argument)
            {
                case "up":
                    return 0;
                case "right":
                    return 1;
                case "down":
                    return 2;
                case "left":
                    return 3;
                default:
                    return 0;
            }
        }

        public static void Gather(Screen Screen, ref Inventory Player_Inventory, Location Current_Location)
        {
            if(Current_Location.item != null)
            {
                Item New_Item = Current_Location.item;
                if(Player_Inventory.Add(New_Item))
                {
                    Screen.Display_Inventory_Contents(Player_Inventory);
                    Screen.Update();
                    Screen.Write_To_Command_Window("You picked up: \"" + New_Item.Name + "\"");
                    return;
                }
                Screen.Write_To_Command_Window("Your inventory is full");
            }
            else
            {
                Screen.Write_To_Command_Window("There is nothing to gather in this location");
            }
        }
    }
}
