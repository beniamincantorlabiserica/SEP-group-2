namespace TheNaturesLastStand
{
    public class NPC
    {
        public string name {get;}
        public string initialDialog {get;}
        public string middleDialog {get;}
        public string finalDialog {get;}

        // INFO
        // 1 - not started
        // 2 - started
        // 3 - done
        public int isQuestDone {set; get;}
        public Quest quest {set; get;}

        public NPC(string name, string initialDialog, string middleDialog, string finalDialog,  Quest quest)
        {
            this.name = name;
            this.initialDialog = initialDialog;
            this.middleDialog = middleDialog;
            this.finalDialog = finalDialog;
            this.quest = quest;
            isQuestDone = 1;
        }

        // TO-DO: the console writeline should be replaced with a message to the GUI
        public void Talk() {
            if (isQuestDone == 1) {
                Console.WriteLine(initialDialog);
                isQuestDone = 2;
            } else if (isQuestDone == 2) {
                Console.WriteLine(middleDialog);
                isQuestDone = 3;
            } else if (isQuestDone == 3) {
                Console.WriteLine(finalDialog);
            }
        }
    }
}