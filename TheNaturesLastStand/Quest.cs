
namespace TheNaturesLastStand;

public class Quest
{
    public QuestState State { get; set; }
    public QuestType Type { get; set; }
    public int BiomeId { get; set; }
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int RewardAmount { get; set; }
    public string[] Dialog { get; set; }
    public string PositiveCommand { get; set; }
    public string NegativeCommand { get; set; }

    /// <summary>
    /// Constructor of class Quest Initializing the attirbutes
    /// </summary>
    /// <param name="PositiveCommand">correct quest advancement command</param>
    /// <param name="NegativeCommand">incorrect quest advancement command</param>
    /// <param name="Dialog">an array storing quest dialogue</param>
    /// <param name="RewardAmount">reward amount for completing the quest</param>
    /// <param name="Name">quest name used for quest display</param>
    /// <param name="Description">quest description for quest display</param>
    /// <param name="Type">which of the quest types the quest is</param>
    /// <param name="State">current quest state</param>
    /// <param name="BiomeId">biome in which the quest is</param>
    /// <param name="ID">specific quest id</param>
    public Quest(int ID, string positiveCommand, string negativeCommand, string[] dialog, int rewardAmount, string name, string description, int biomeId, QuestType type = QuestType.Regular)
    {
        PositiveCommand = positiveCommand;
        NegativeCommand = negativeCommand;
        Dialog = dialog;
        RewardAmount = rewardAmount;
        Name = name;
        Description = description;
        Type = type;
        State = QuestState.NotSeen;
        BiomeId = biomeId;
        this.ID = ID;
    }
}

public enum QuestState
{
    NotSeen,
    Seen,
    Talking,
    Active,
    Done
}

public enum QuestType
{
    NpcQuest,
    Regular,
    ItemQuest
}
