
using System.Data.Common;

namespace TheNaturesLastStand
{
   public class Location
   {
      //  private Item item;
      public string name { get;}
      private Quest quest;
      private Dictionary<string,Location> exits;
      private int id;

      public Location(string name)
      {
            this.name = name;
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
         if(neighbor!=null)
         {
            exits[direction] = neighbor;
         }
      }

   }
}