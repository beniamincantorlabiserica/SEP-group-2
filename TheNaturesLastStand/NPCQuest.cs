namespace TheNaturesLastStand;

public class NPCQuest : Quest
{
    public NPCQuest(string positiveCommand, string negativeCommand, string[] dialog, int rewardAmount, bool seen) : base(positiveCommand, negativeCommand, dialog, rewardAmount)
    {
        Seen = seen;
    }

    public bool Seen { get; set; }
    
}
