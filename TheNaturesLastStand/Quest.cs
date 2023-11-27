
namespace TheNaturesLastStand;

public class Quest
{
    public QuestState State { get; set; }
    public QuestType Type { get; set; }
    public int RewardAmount { get; set; }
    public string[] Dialog { get; set; }
    public string PositiveCommand { get; set; }
    public string NegativeCommand { get; set; }

    public Quest(string positiveCommand, string negativeCommand, string[] dialog, int rewardAmount)
    {
        PositiveCommand = positiveCommand;
        NegativeCommand = negativeCommand;
        Dialog = dialog;
        RewardAmount = rewardAmount;
        State = QuestState.NotSeen;;
    }
}

public enum QuestState
{
    NotSeen,
    Seen,
    Active,
    Done
}

public enum QuestType
{
    NpcQuest,
    Regular
}