
namespace TheNaturesLastStand;

public class Location
{
   public Location(int id, string name, string description, Quest quest, Location rightLocation, Location leftLocation, Biome biome, Location upLocation, Location downLocation)
   {
      Id = id;
      Name = name;
      Description = description;
      Quest = quest;
      RightLocation = rightLocation;
      LeftLocation = leftLocation;
      Biome = biome;
      UpLocation = upLocation;
      DownLocation = downLocation;
   }

   public int Id { get; set; }
   public string Name { get; set; }
   public string Description { get; set; }
   public Quest Quest { get; set; }
   public Location RightLocation { get; set; }
   public Location LeftLocation { get; set; }
   public Location UpLocation { get; set; }
   public Location DownLocation { get; set; }
   public Biome Biome { get; set; }

}