
namespace TheNaturesLastStand
{
    public class Location
    {
      public string name {get;}
      public Item item {get; set;}
      public Quest quest {get; set;}
      private Dictionary <string,Location> exits {get; set;}

      private int id {get; set;}

      public Location(string name, Quest? quest, Item? item )
      {
            this.name = name;
            this.quest = quest;
            this.exits = new Dictionary<string, Location>();
            this.id = id; 
            this.item = item;
      }

      public void SetExits(Location? up, Location? down, Location? right, Location? left)
      {
         SetExit("up", up);
         SetExit("down", down);
         SetExit("left", left);
         SetExit("right", right);

      }
      private void SetExit(string direction, Location? neighbor)
      {
         if(neighbor != null)
         {
            exits[direction] = neighbor;
         }
      }

    }
}