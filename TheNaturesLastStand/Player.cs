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
        if (CurrentLocation.Quest.State == QuestState.Talking)
        {
            if (Command == "accept")
            {
                CurrentLocation.Quest.State = QuestState.Active;
                ActiveQuests.Add(CurrentLocation.Quest);
            }else if (Command != "decline")
            {
                InvalidCommand();
            }
        } 
        else
        {
            switch (Command)
            {
                case "tutorial": 
                    ScreenManager.Tutorial(); 
                    break;
                case "move right":
                    if (CurrentLocation.RightLocation != null)
                    {
                        CurrentLocation = CurrentLocation.RightLocation;
                        UpdateScreen(LocationSwitchMessage());
                    }
                    break;
                case "move left":
                    if (CurrentLocation.LeftLocation != null)
                    {
                        CurrentLocation = CurrentLocation.LeftLocation;
                        UpdateScreen(LocationSwitchMessage());
                    }
                    break;
                case "move up":
                    if (CurrentLocation.UpLocation != null)
                    {
                        CurrentLocation = CurrentLocation.UpLocation;
                        UpdateScreen(LocationSwitchMessage());
                    }
                    break;
                case "move down":
                    if (CurrentLocation.DownLocation != null)
                    {
                        CurrentLocation = CurrentLocation.DownLocation;
                        UpdateScreen(LocationSwitchMessage());
                    }
                    break;
                case "look":
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
                    else if(CurrentLocation.Quest.Type == QuestType.Regular)
                    {
                        switch (CurrentLocation.Quest.State)
                        {
                            case QuestState.NotSeen:
                                ScreenManager.DisplayMessage(CurrentLocation.Quest.Dialog[0]);
                                CurrentLocation.Quest.State = QuestState.Talking;
                                break;
                            case QuestState.Active:
                                ScreenManager.DisplayMessage(CurrentLocation.Quest.Dialog[1]);
                                CurrentLocation.Quest.State = QuestState.Talking;
                                break;
                            case QuestState.Done:
                                UpdateScreen("You already completed this Quest. Try to find more quests in this biome");
                                break;
                            default:
                                CurrentLocation.Quest.State = QuestState.Seen;
                                break;
                        }
                    }
                    break;
                case "talk":
                    if (CurrentLocation.Quest.Type == QuestType.NpcQuest)
                    {
                        switch (CurrentLocation.Quest.State)
                        {
                            case QuestState.Seen:
                                ScreenManager.DisplayMessage(CurrentLocation.Quest.Dialog[0]);
                                CurrentLocation.Quest.State = QuestState.Talking;
                                break;
                            case QuestState.Active:
                                ScreenManager.DisplayMessage(CurrentLocation.Quest.Dialog[1]);
                                break;
                            case QuestState.Done:
                                UpdateScreen("Your help was very welcome. Thank you for fighting for the planet!");
                                break;
                            default: 
                                InvalidCommand();
                                break;
                        }
                    }
                    break;
                case "help":
                    ScreenManager.ShowHelp();
                    break;
                case "quit":
                    ScreenManager.DisplayGameOver();
                    break;
            }
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
        ScreenManager.UpdateLocationInfo(CurrentLocation);
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
        ScreenManager.UpdateScreen(Balance, ActiveQuests, CurrentLocation.Biome.Name, CurrentLocation.Name, message);
    }
    
}