
namespace TheNaturesLastStand
{
    class Quest
    {
        private string name;
        private string description;
        private bool done;
        public Quest(string name, string description)
        {
            this.name = name;
            this.description = description;
            done = false;
        }
        public bool IsDone()
        {
            return true;
        }

    }
}