namespace TheNaturesLastStand;

public class Biome
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Location> LocationsList { get; set; }
    public int MinimumMoneyThreshold { get; set; }

    /// <summary>
    /// Constructor of class Biome Initializing the attirbutes
    /// </summary>
    /// <param name="name">name of biome</param>
    /// <param name="description">dsecription of biome</param>
    /// <param name="minimumMoneyThreshold">the minimum amount of money needed to enter this biome</param>
    public Biome(string name, string description, int minimumMoneyThreshold)
    {
        Name = name;
        Description = description;
        MinimumMoneyThreshold = minimumMoneyThreshold;
        LocationsList = new List<Location>();
    }

}