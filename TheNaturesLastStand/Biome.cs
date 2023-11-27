namespace TheNaturesLastStand;

public class Biome
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Location> LocationsList { get; set; }
    public int MinimumMoneyThreshold { get; set; }

    public Biome(string name, string description, int minimumMoneyThreshold, List<Location> locations)
    {
        Name = name;
        Description = description;
        LocationsList = locations;
        MinimumMoneyThreshold = minimumMoneyThreshold;
    }
}