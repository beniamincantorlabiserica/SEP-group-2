namespace TheNaturesLastStand
{
   public class Location
   {
      public string name {get;}

      //  private Item item {get; set;}
      private Quest quest {get; set;}
      private Dictionary <string,Location> exits {get; set;}
      private int id {get; set;}

      public Location(string name, Quest quest, Dictionary<string,Location> exits, int id /*, Item item*/ )
      {
            this.name = name;
            this.quest = quest;
            this.exits = exits;
            this.id = id; 
            //this.item = item;
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