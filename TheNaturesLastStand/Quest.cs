
namespace TheNaturesLastStand;

public class Quest
{
    public QuestState State { get; set; }
    public QuestType Type { get; set; }
    public int BiomeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int RewardAmount { get; set; }
    public string[] Dialog { get; set; }
    public string PositiveCommand { get; set; }
    public string NegativeCommand { get; set; }

    public Quest(string positiveCommand, string negativeCommand, string[] dialog, int rewardAmount, string name, string description, int biomeId, QuestType type = QuestType.Regular)
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
    Regular
}