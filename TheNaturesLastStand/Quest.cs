
namespace TheNaturesLastStand
{
    public class Quest
    {
        public string name;
        public string description;
        public bool done {set; get;}
        public Quest(string name, string description)
        {
            this.name = name;
            this.description = description;
            done = false;
        }
    }
}