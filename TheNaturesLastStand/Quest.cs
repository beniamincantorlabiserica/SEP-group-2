
namespace TheNaturesLastStand
{
    public class Quest
    {
        public string name;
        public string description;
        public string choiceGood;
        public string choiceBad;
        public string? choiceCurrent {set; get;}
        public bool done {set; get;}
        public int currentQuestStage;
        public Quest(string name, string description, string choiceGood, string choiceBad, int currentQuestStage)
        {
            this.name = name;
            this.description = description;
            this.choiceGood = choiceGood;
            this.choiceBad = choiceBad;
            done = false;
            this.currentQuestStage = currentQuestStage;
        }

        public bool VerifyChoice(string choiceCurrent)
        {
            if (choiceCurrent == this.choiceGood)
                return true;
            
            else if (choiceCurrent == this.choiceBad)
                return true;

            else
                return false;
        }

        public bool IsDone(string choiceCurrent)
        {
            if (choiceCurrent == this.choiceGood)
                return done = true;

            else
                return done = false;
        }

        public int QuestStageIncrease()
        {
            if (currentQuestStage == 1)
                return currentQuestStage++;
            
            else if (currentQuestStage == 2)
                return currentQuestStage++;

            else
                return currentQuestStage;
        }

        public int QuestStageDecrease()
        {
            if (currentQuestStage == 1)
                return currentQuestStage;
            
            else if (currentQuestStage == 2)
                return currentQuestStage--;

            else
                return currentQuestStage;
        }
    }
}