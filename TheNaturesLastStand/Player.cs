using System.Linq.Expressions;

namespace TheNaturesLastStand;

public class Player
{
    public int Balance { get; set; }
    public Location CurrentLocation { get; set; }
    public List<Quest> ActiveQuests { get; set; }
    public List<Biome> BiomeList { get; set; }
    private ScreenManager ScreenManager;
    
    

    public Player(ScreenManager ScreenManager)
    {
        this.ScreenManager = ScreenManager;
        UpdateLocationInfo();
    }

    public void Init()
    { 
        // create biomes, locations and quests
        ScreenManager.Init();
    }

    public void DoCommand(string Command)
    {
        if (CurrentLocation.Quest.State == QuestState.Talking)
        {
            if (Command == "accept")
            {
                ActiveQuests.Add(CurrentLocation.Quest);
                CurrentLocation.Quest.State = QuestState.Active;
                ScreenManager.DisplayMessage("You accepted this quest. It will be added in your active quests list");
            } else if (Command == "decline")
            {
                ScreenManager.DisplayMessage("I am sorry to hear that...I guess somebody else has to save the planet");
                CurrentLocation.Quest.State = QuestState.Seen;
            }
            else
            {
                InvalidCommand();
                CurrentLocation.Quest.State = QuestState.Seen;
            }
        } else if (CurrentLocation.Quest is { Type: QuestType.Regular, State: QuestState.Seen })
        {
            if (Command == "accept")
            {
                ActiveQuests.Add(CurrentLocation.Quest);
                CurrentLocation.Quest.State = QuestState.Active;
                ScreenManager.DisplayMessage("You accepted this quest. It will be added in your active quests list");
            } else if (Command == "decline")
            {
                ScreenManager.DisplayMessage("I am sorry to hear that...I guess somebody else has to save the planet");
                CurrentLocation.Quest.State = QuestState.NotSeen;
            }
            else
            {
                InvalidCommand();
                CurrentLocation.Quest.State = QuestState.NotSeen;
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
                        ScreenManager.DisplayMessage(LocationSwitchMessage());
                        UpdateLocationInfo();
                    }
                    break;
                case "move left":
                    if (CurrentLocation.LeftLocation != null)
                    {
                        CurrentLocation = CurrentLocation.LeftLocation;
                        ScreenManager.DisplayMessage(LocationSwitchMessage());
                        UpdateLocationInfo();
                    }
                    break;
                case "move up":
                    if (CurrentLocation.UpLocation != null)
                    {
                        CurrentLocation = CurrentLocation.UpLocation;
                        ScreenManager.DisplayMessage(LocationSwitchMessage());
                        UpdateLocationInfo();
                    }
                    break;
                case "move down":
                    if (CurrentLocation.DownLocation != null)
                    {
                        CurrentLocation = CurrentLocation.DownLocation;
                        ScreenManager.DisplayMessage(LocationSwitchMessage());
                        UpdateLocationInfo();
                    }
                    break;
                case "look":
                    if (CurrentLocation.Quest.Type == QuestType.NpcQuest)
                    {
                        switch (CurrentLocation.Quest.State)
                        {
                            case QuestState.NotSeen: 
                                CurrentLocation.Quest.State = QuestState.Seen;
                                ScreenManager.DisplayMessage($"Someone is here, try talking by using \"talk\" command.");
                                break;
                            default: 
                                ScreenManager.DisplayMessage($"Someone is here, try talking by using \"talk\" command.");
                                break;
                        }
                    }
                    else if(CurrentLocation.Quest.Type == QuestType.Regular)
                    {
                        switch (CurrentLocation.Quest.State)
                        {
                            case QuestState.NotSeen:
                                ScreenManager.DisplayMessage(CurrentLocation.Quest.Dialog[0]);
                                break;
                            case QuestState.Active:
                                ScreenManager.DisplayMessage(CurrentLocation.Quest.Dialog[1]);
                                break;
                            case QuestState.Done:
                                ScreenManager.DisplayMessage(
                                    "You already completed this Quest. Try to find more quests in this biome");
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
                                ScreenManager.DisplayMessage("Your help was very welcome. Thank you for fighting for the planet!");
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

    private String LocationSwitchMessage()
    {
        return $"{CurrentLocation.Name} \n {CurrentLocation.Description} \n";
    }

    private void UpdateLocationInfo()
    {
        ScreenManager.UpdateLocationInfo(CurrentLocation);
    }

    private void InvalidCommand()
    {
        ScreenManager.DisplayMessage("Invalid Command");
    }
    
    
}