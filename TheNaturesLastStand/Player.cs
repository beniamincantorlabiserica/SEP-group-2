namespace TheNaturesLastStand;

public class Player
{
    public int Balance { get; set; }
    public Location CurrentLocation { get; set; }
    public List<Quest> ActiveQuests { get; set; }
    public List<Biome> BiomeList { get; set; }
    public ScreenManager ScreenManager { get; }
    
    

    public Player(ScreenManager ScreenManager)
    {
        this.ScreenManager = ScreenManager;
        UpdateLocationInfo();
    }

    public void Init()
    { 
        CreateStoryline();
        ScreenManager.Init();
    }

    public void DoCommand(string Command)
    {
        if (Command == "move right")
        {
            if (CurrentLocation.RightLocation != null)
            {
                CurrentLocation = CurrentLocation.RightLocation;
                UpdateScreen(LocationSwitchMessage());
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
                CurrentLocation = CurrentLocation.LeftLocation;
                UpdateScreen(LocationSwitchMessage());
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
                CurrentLocation = CurrentLocation.UpLocation;
                UpdateScreen(LocationSwitchMessage());
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
                CurrentLocation = CurrentLocation.DownLocation;
                UpdateScreen(LocationSwitchMessage());
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
            CurrentLocation.Quest.State = QuestState.Done;
            Balance += CurrentLocation.Quest.RewardAmount;
            UpdateScreen(CurrentLocation.Quest.Dialog[2]);
        }
        else if (Command == CurrentLocation.Quest.NegativeCommand)
        {
            UpdateScreen("Uhh...actually it would be nice of you if you did this...");
        }
        else if (Command == "tutorial")
        {
            ScreenManager.DisplayTutorial();
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
                                     "\n\nWould you like to accept this quest? \n\n >accept \n >decline)");
                        CurrentLocation.Quest.State = QuestState.Seen;
                        break;
                    case QuestState.Seen:
                        UpdateScreen(CurrentLocation.Quest.Description +
                                     "\n\nWould you like to accept this quest? \n\n >accept \n >decline)");
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
            ScreenManager.DisplayHelp();
        }
        else if (Command == "quit")
        {
            ScreenManager.DisplayGameOver();
        }
    }

    // TO-DO
    // check when changing biome to add biome description otherwise add only location description
    private string LocationSwitchMessage()
    {
        return $"{CurrentLocation.Name} \n {CurrentLocation.Description} \n";
    }

    private void UpdateLocationInfo()
    {
        //ScreenManager.UpdateLocationInfo(CurrentLocation);
    }

    private void InvalidCommand()
    {
        UpdateScreen("Invalid Command");
    }

    public void CreateStoryline()
    {
        // create locations, biomes and quests
    }

    private void UpdateScreen(string message)
    {
        List<string> activeQuestsStringList = new List<string>();
        foreach (var quest in ActiveQuests)
        {
            activeQuestsStringList.Add(quest.Name);
        }
        ScreenManager.UpdateScreen(Balance, activeQuestsStringList, CurrentLocation.Biome.Name, CurrentLocation.Name, message);
    }
    
}