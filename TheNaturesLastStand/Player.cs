namespace TheNaturesLastStand;

public class Player
{
    public int Balance { get; set; }
    public Location CurrentLocation { get; set; }
    public List<Quest> ActiveQuests { get; set; }
    public List<Biome> BiomeList { get; set; }
    public ScreenManager ScreenManager { get; }
    public ContentProvider ContentProvider { get; set; }
    
    

    public Player(ScreenManager ScreenManager)
    {
        this.ScreenManager = ScreenManager;
    }

    public void Init()
    {
        ContentProvider = new ContentProvider();
        ActiveQuests = new List<Quest>();
        CurrentLocation = ContentProvider.GetStartingLocation();
    }

    public void DoCommand(string Command)
    {
        if (Command == "move right")
        {
            if (CurrentLocation.RightLocation != null)
            {
                if (CurrentLocation.RightLocation.Biome.Name != CurrentLocation.Biome.Name)
                {
                    if (CurrentLocation.RightLocation.Biome.MinimumMoneyThreshold < Balance)
                    {
                        CurrentLocation = CurrentLocation.RightLocation;
                        UpdateScreen(LocationSwitchMessage());
                    }
                    else
                    {
                        UpdateScreen("Uhh, seems like you need more money to pass to a new biome. Try completing more quests.");
                    }
                }
                else
                {
                    CurrentLocation = CurrentLocation.RightLocation;
                    UpdateScreen(LocationSwitchMessage());
                }
                
            }
            else
            {
                UpdateScreen("Sorry...no location in this direction.");
            }
        }
        else if (Command == "move left")
        {
            if (CurrentLocation.LeftLocation != null)
            {
                if (CurrentLocation.LeftLocation.Biome.Name != CurrentLocation.Biome.Name)
                {
                    if (CurrentLocation.LeftLocation.Biome.MinimumMoneyThreshold < Balance)
                    {
                        CurrentLocation = CurrentLocation.LeftLocation;
                        UpdateScreen(LocationSwitchMessage());
                    }
                    else
                    {
                        UpdateScreen("Uhh, seems like you need more money to pass to a new biome. Try completing more quests.");
                    }
                }
                else
                {
                    CurrentLocation = CurrentLocation.LeftLocation;
                    UpdateScreen(LocationSwitchMessage());
                }
            }
            else
            {
                UpdateScreen("Sorry...no location in this direction.");
            }
        }
        else if (Command == "move up")
        {
            if (CurrentLocation.UpLocation != null)
            {
                if (CurrentLocation.UpLocation.Biome.Name != CurrentLocation.Biome.Name)
                {
                    if (CurrentLocation.UpLocation.Biome.MinimumMoneyThreshold < Balance)
                    {
                        CurrentLocation = CurrentLocation.UpLocation;
                        UpdateScreen(LocationSwitchMessage());
                    }
                    else
                    {
                        UpdateScreen("Uhh, seems like you need more money to pass to a new biome. Try completing more quests.");
                    }
                }
                else
                {
                    CurrentLocation = CurrentLocation.UpLocation;
                    UpdateScreen(LocationSwitchMessage());
                }
            }
            else
            {
                UpdateScreen("Sorry...no location in this direction.");
            }
        }
        else if (Command == "move down")
        {
            if (CurrentLocation.DownLocation != null)
            {
                if (CurrentLocation.DownLocation.Biome.Name != CurrentLocation.Biome.Name)
                {
                    if (CurrentLocation.DownLocation.Biome.MinimumMoneyThreshold < Balance)
                    {
                        CurrentLocation = CurrentLocation.DownLocation;
                        UpdateScreen(LocationSwitchMessage());
                    }
                    else
                    {
                        UpdateScreen("Uhh, seems like you need more money to pass to a new biome. Try completing more quests.");
                    }
                }
                else
                {
                    CurrentLocation = CurrentLocation.DownLocation;
                    UpdateScreen(LocationSwitchMessage());
                }
            }
            else
            {
                UpdateScreen("Sorry...no location in this direction.");
            }
        }
        else if (Command == "accept")
        {
            if (CurrentLocation.Quest.Type == QuestType.NpcQuest)
            {
                if (CurrentLocation.Quest.State == QuestState.Talking)
                {
                    CurrentLocation.Quest.State = QuestState.Active;
                    ActiveQuests.Add(CurrentLocation.Quest);
                    UpdateScreen("You accepted this quest, to do something about it, try looking again...");
                }
                else
                {
                    InvalidCommand();
                }
            }
            else
            {
                CurrentLocation.Quest.State = QuestState.Active;
                ActiveQuests.Add(CurrentLocation.Quest);
                UpdateScreen("You accepted this quest, to do something about it, try looking again...");
            }
        }
        else if (Command == "decline")
        {
            if (CurrentLocation.Quest.Type == QuestType.NpcQuest)
            {
                CurrentLocation.Quest.State = QuestState.Seen;
            }

            UpdateScreen("Sorry to heat that...guess somebody else has to save the world...");
        }
        else if (Command == CurrentLocation.Quest.PositiveCommand)
        {
            if (CurrentLocation.Quest.State != QuestState.Done)
            {
                CurrentLocation.Quest.State = QuestState.Done;
                Balance += CurrentLocation.Quest.RewardAmount;
                ActiveQuests.Remove(CurrentLocation.Quest);
                UpdateScreen(CurrentLocation.Quest.Dialog[2]);
            }
            else
            {
                InvalidCommand();
            }

        }
        else if (Command == CurrentLocation.Quest.NegativeCommand)
        {
            UpdateScreen("Uhh...actually it would be nice of you if you did this...");
        }
        else if (Command == "tutorial")
        {
            UpdateScreen( 
                         "Welcome to Natures' Last Stand console game.\nAs you might know, our planet is suffering because we don't take care of it as we should.\nBut no worries, you've came to our help and we are ready to save mother nature with your help.\nEverytime you move around the island you'll find new quests, once completing them you'll get a reward helping you advancing to new biomes!\n\nTry using \"help\" to get aquinted with the game's commands"
                         );
        }
        else if (Command == "look")
        {
            if (CurrentLocation.Quest.Type == QuestType.NpcQuest)
            {
                switch (CurrentLocation.Quest.State)
                {
                    case QuestState.NotSeen:
                        CurrentLocation.Quest.State = QuestState.Seen;
                        UpdateScreen($"Someone is here, try talking by using \"talk\" command.");
                        break;
                    default:
                        UpdateScreen($"Someone is here, try talking by using \"talk\" command.");
                        break;
                }
            }
            else if (CurrentLocation.Quest.Type == QuestType.Regular)
            {
                switch (CurrentLocation.Quest.State)
                {
                    case QuestState.NotSeen:
                        UpdateScreen(CurrentLocation.Quest.Description +
                                     "\n\nWould you like to accept this quest? \n\n >accept \n >decline");
                        CurrentLocation.Quest.State = QuestState.Seen;
                        break;
                    case QuestState.Seen:
                        UpdateScreen(CurrentLocation.Quest.Description +
                                     "\n\nWould you like to accept this quest? \n\n >accept \n >decline");
                        CurrentLocation.Quest.State = QuestState.Seen;
                        break;
                    case QuestState.Active:
                        UpdateScreen(CurrentLocation.Quest.Description + "\n\n >" +
                                     CurrentLocation.Quest.PositiveCommand + "\n >" +
                                     CurrentLocation.Quest.NegativeCommand);
                        break;
                    case QuestState.Done:
                        UpdateScreen(
                            "You already completed this Quest. Try going to another location find more quests");
                        break;
                }
            }
        }
        else if (Command == "talk")
        {
            if (CurrentLocation.Quest.Type == QuestType.NpcQuest)
            {
                switch (CurrentLocation.Quest.State)
                {
                    case QuestState.Seen:
                        UpdateScreen(CurrentLocation.Quest.Dialog[0] + "\n\n >accept\n >decline");
                        CurrentLocation.Quest.State = QuestState.Talking;
                        break;
                    case QuestState.Active:
                        UpdateScreen(CurrentLocation.Quest.Dialog[1]);
                        break;
                    case QuestState.Done:
                        UpdateScreen("Your help was very welcome. Thank you for fighting for the planet!");
                        break;
                }
            }
        }
        else if (Command == "help")
        {
            UpdateScreen(
                "I am your helping deer, and I am ready to help. Here's an overview on what actions you could do:\n>move right/up/down/left - let's you move to the specified location\n>look - with this command you are looking around to check stuff\n>talk - let's you talk with NPCs in the current location\n>accept - will accept a quest and add it in your active quest list\n>decline - will decline a quest\n\nWhen you want to complete a quest use the provided commands to complete or not the quest.\n\nGood luck adventurer\nI will be always available to you, just call me with a simple \"help\""
                );
        }
        else if (Command == "quit")
        {
            ScreenManager.DisplayGameOver();
        }
        else
        {
            InvalidCommand();
        }
    }

    // TO-DO
    // check when changing biome to add biome description otherwise add only location description
    private string LocationSwitchMessage()
    {
        return CurrentLocation.Description;
    }

    private void InvalidCommand()
    {
        UpdateScreen("Invalid Command");
    }

    private void UpdateScreen(string message)
    {
        List<string> activeQuestsStringList = new List<string>();
        foreach (var quest in ActiveQuests)
        {
            activeQuestsStringList.Add(quest.Name);
        }

        
       //TODO 
       // send the available exits list to screen manager
        ScreenManager.UpdateScreen(Balance, activeQuestsStringList, CurrentLocation.Biome.Name, CurrentLocation.Name, message);
    }

    private List<string> GetAvailableExits()
    {
        List<string> availableExits = new List<string>();
        if (CurrentLocation.UpLocation != null)
        {
            availableExits.Add(">up");
        }
        if (CurrentLocation.DownLocation != null)
        {
            availableExits.Add(">down");
        }
        if (CurrentLocation.RightLocation != null)
        {
            availableExits.Add(">right");
        }
        if (CurrentLocation.LeftLocation != null)
        {
            availableExits.Add(">left");
        }

        return availableExits;
    }
    
}