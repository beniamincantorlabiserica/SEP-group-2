namespace TheNaturesLastStand;

public class Player
{
    public int Balance { get; set; }
    public Location CurrentLocation { get; set; }
    public List<Quest> ActiveQuests { get; set; }
    public List<Biome> BiomeList { get; set; }
    public ScreenManager ScreenManager { get; }
    public ContentProvider ContentProvider { get; set; }
    private HashSet<Location> SeenLocations;
    private HashSet<Quest> SeenQuests;
    private HashSet<Biome> SeenBiome;
    private int LocationCount;
    private int QuestCount;
    private int BiomeCount;
    private double Progress;
    
    

    /// <summary>
    /// Constructor for Player class initializing the ScreenManager with the passed argument
    /// </summary>
    /// <param name="ScreenManager">ScreenManager reference passed from initializng class</param>
    public Player(ScreenManager ScreenManager)
    {
        this.ScreenManager = ScreenManager;  
    }

    /// <summary>
    /// Initialize the variables needed in the class for performing logic
    /// </summary>
    public void Init()
    {
        ContentProvider = new ContentProvider();
        ActiveQuests = new List<Quest>();
        SeenQuests = new HashSet<Quest>();
        SeenBiome = new HashSet<Biome>();
        SeenLocations = new HashSet<Location>();
        CurrentLocation = ContentProvider.GetStartingLocation();
        SeenLocations.Add(CurrentLocation);
        SeenBiome.Add(CurrentLocation.Biome);
        (LocationCount, QuestCount) = ContentProvider.GetMaxProgressState();
        BiomeCount = 5;
        Progress = 0;
    }

    /// <summary>
    /// The function takes a command as a string and perform different activities dependent on the command
    /// </summary>
    /// <param name="Command">string entered by the user indicating what the game should do</param>
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
                        SeenLocations.Add(CurrentLocation.RightLocation);
                        SeenBiome.Add(CurrentLocation.RightLocation.Biome);
                        CurrentLocation = CurrentLocation.RightLocation;
                        UpdateScreen(LocationSwitchMessage(1));
                    }
                    else
                    {
                        UpdateScreen("Uhh, seems like you need more money to pass to a new biome. Try completing more quests.");
                    }
                }
                else
                {
                    SeenLocations.Add(CurrentLocation.RightLocation);
                    CurrentLocation = CurrentLocation.RightLocation;
                    UpdateScreen(LocationSwitchMessage(0));
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
                        SeenLocations.Add(CurrentLocation.LeftLocation);
                        SeenBiome.Add(CurrentLocation.LeftLocation.Biome);
                        CurrentLocation = CurrentLocation.LeftLocation;
                        UpdateScreen(LocationSwitchMessage(1));
                    }
                    else
                    {
                        UpdateScreen("Uhh, seems like you need more money to pass to a new biome. Try completing more quests.");
                    }
                }
                else
                {
                    SeenLocations.Add(CurrentLocation.LeftLocation);
                    CurrentLocation = CurrentLocation.LeftLocation;
                    UpdateScreen(LocationSwitchMessage(0));
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
                        SeenLocations.Add(CurrentLocation.UpLocation);
                        SeenBiome.Add(CurrentLocation.UpLocation.Biome);
                        CurrentLocation = CurrentLocation.UpLocation;
                        UpdateScreen(LocationSwitchMessage(1));
                    }
                    else
                    {
                        UpdateScreen("Uhh, seems like you need more money to pass to a new biome. Try completing more quests.");
                    }
                }
                else
                {
                    SeenLocations.Add(CurrentLocation.UpLocation);
                    CurrentLocation = CurrentLocation.UpLocation;
                    UpdateScreen(LocationSwitchMessage(0));
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
                        SeenLocations.Add(CurrentLocation.DownLocation);
                        SeenBiome.Add(CurrentLocation.DownLocation.Biome);
                        CurrentLocation = CurrentLocation.DownLocation;
                        UpdateScreen(LocationSwitchMessage(1));
                    }
                    else
                    {
                        UpdateScreen("Uhh, seems like you need more money to pass to a new biome. Try completing more quests.");
                    }
                }
                else
                {
                    SeenLocations.Add(CurrentLocation.DownLocation);
                    CurrentLocation = CurrentLocation.DownLocation;
                    UpdateScreen(LocationSwitchMessage(0));
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
                SeenQuests.Add(CurrentLocation.Quest);
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
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Current's location description and biome description when needed </returns>
    private string LocationSwitchMessage(int changingBiome)
    {
        if (changingBiome == 1)
        {
            return CurrentLocation.Biome.Description + " " + CurrentLocation.Description;
        }
        else
        {
            return CurrentLocation.Description;
        }
        
    }

    /// <summary>
    /// Updates the screen with an invalid command
    /// </summary>
    private void InvalidCommand()
    {
        UpdateScreen("Invalid Command");
    }

    /// <summary>
    /// Updates the screen with all the data needed and the message sent when calling the function
    /// </summary>
    /// <param name="message">string that the function is called with that will be displayed on the screen</param>
    private void UpdateScreen(string message)
    {
        List<string> activeQuestsStringList = new List<string>();
        foreach (var quest in ActiveQuests)
        {
            activeQuestsStringList.Add(quest.Name);
        }
        
        CalculateProgress();
        ScreenManager.UpdateScreen(Balance, activeQuestsStringList, CurrentLocation.Biome.Name, CurrentLocation, message, Progress);
    }

    /// <summary>
    /// Calculates current progress
    /// scorePerTask is calculating how much percentage is every quest/location/biome worth
    /// The progress is calculates using total quests/locations/biome discovered/completed by the user multiplied by scorePerTask
    /// </summary>
    private void CalculateProgress()
    {
        double scorePerTask = 100 / (BiomeCount + LocationCount + QuestCount);
        int totalTasksCompleted = SeenBiome.Count + SeenLocations.Count + SeenQuests.Count;
        Progress = scorePerTask * totalTasksCompleted;
    }
    
}