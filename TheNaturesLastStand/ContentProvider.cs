namespace TheNaturesLastStand;

public class ContentProvider
{
    private List<Quest> _quests;
    private List<Location> _locations;
    private List<Biome> _biomes;
    
    public ContentProvider()
    {
        _quests = GenerateQuests();
        _locations = GenerateLocations();
        _biomes = GenerateBiomes();
        LinkContent();
    }

    private void LinkContent()
    {
        _locations[0].UpLocation = _locations[1];
        _locations[1].LeftLocation = _locations[2];
        _locations[0].Biome = _biomes[0];
        _locations[1].Biome = _biomes[0];
        _locations[2].Biome = _biomes[0];
        _locations[0].Quest = _quests[0];
        _locations[1].Quest = _quests[1];
        _locations[2].Quest = _quests[2];
    }

    public Location GetStartingLocation()
    {
        return _locations[0];
    }

    private List<Quest> GenerateQuests()
    {
        var quests = new List<Quest>();
        quests.Add(new Quest("do", "not interested", new []{"Dialog1x", "Dialog2x", "Dialog3x"}, 15, "Quest 1", "Quest 1 Description"));
        quests.Add(new Quest("do", "not interested", new []{"Dialog1y", "Dialog2y", "Dialog3y"}, 25, "Quest 2", "Quest 2 Description"));
        quests.Add(new Quest("do", "not interested", new []{"Dialog1z", "Dialog2z", "Dialog3z"}, 35, "Quest 3", "Quest 3 Description"));
        return quests;
    }

    private List<Location> GenerateLocations()
    {
        var locations = new List<Location>();
        locations.Add(new Location(1, "Location 1", "Location 1 Description", null, null, null, null, null, null));
        locations.Add(new Location(2, "Location 2", "Location 2 Description", null, null, null, null, null, null));
        locations.Add(new Location(3, "Location 3", "Location 3 Description", null, null, null, null, null, null));
        return locations;
    }

    private List<Biome> GenerateBiomes()
    {
        var biomes = new List<Biome>();
        biomes.Add(new Biome("Biome 1", "Biome 1 Description", 0, _locations));
        return biomes;
    }
}