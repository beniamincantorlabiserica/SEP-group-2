
namespace TheNaturesLastStand
{
    public class Quest
    {
        private string name;
        private string description;
        private bool done {set; get;}
        public Quest(string name, string description)
        {
            this.name = name;
            this.description = description;
            done = false;
        }
    }
}