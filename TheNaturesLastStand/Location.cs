
namespace TheNaturesLastStand
{
    public class Location
    {
        //  private Item[] items=new Item[20];
        public string name { get;}
        public string description { get;}
        public List<Quest> quests { set; get;}
        public Stack<Item> Items = new Stack<Item>();
        
        private int currentAdvancements;
        private int totalAdvancements;
        public Location(string name, string description, int totalAdvancements, List<Quest> quests, Stack<Item> Items)
        {
            this.name = name;
            this.description = description;
            this.totalAdvancements = totalAdvancements;
            currentAdvancements = 0;
            this.quests = quests;
            this.Items = Items;
        }

        public Location(string name, string description)
        {
            this.name = name;
            this.description = description;
            this.totalAdvancements = 0;
            this.currentAdvancements = 0;
            this.quests = new List<Quest>();
            this.Items = new Stack<Item>();
        }
    }
}