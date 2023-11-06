
namespace TheNaturesLastStand
{
   public class Location
   {
      //  private Item[] items=new Item[20];
      public string name { get;}
      public string description { get;}
      public List<Quest> quests { set; get;}
      private int currentAdvancements;
      private int totalAdvancements;
      public Location(string name, string description, int totalAdvancements, List<Quest> quests)
      {
            this.name = name;
            this.description = description;
            this.totalAdvancements = totalAdvancements;
            currentAdvancements = 0;
            this.quests = quests;
      }
   }
}