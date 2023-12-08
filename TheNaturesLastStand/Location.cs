
namespace TheNaturesLastStand;

public class Location
{

   /// <summary>
   /// Constructor of class Location Initializing the attirbutes
   /// </summary>
   /// <param name="Id">location id</param>
   /// <param name="Name">location name</param>
   /// <param name="Description">location description</param>
   /// <param name="Biome">what biome the location is in</param>
   /// <param name="Item">if the location has an item</param>
   public Location(int id, string name, string description, Biome? biome)
   {
      Id = id;
      Name = name;
      Description = description;
      Biome = biome;
      Item = null;
   }

   public int Id { get; set; }
   public string Name { get; set; }
   public string Description { get; set; }
   public Quest? Quest { get; set; }
   public Item? Item { get; set; }
   public Location? RightLocation { get; set; }
   public Location? LeftLocation { get; set; }
   public Location? UpLocation { get; set; }
   public Location? DownLocation { get; set; }
   public Biome? Biome { get; set; }

}