using System.Data;

namespace TheNaturesLastStand;

public class ContentProvider
{
    private readonly List<Quest> _quests = new();
    private readonly List<Location> _locations = new();
    private readonly List<Biome> _biomes = new();
    private readonly bool debugMode = false;
    public ContentProvider()
    {
        GenerateBiomes();
        GenerateQuests();
        GenerateLocations();
        LinkContent();
        if (debugMode)
        {
            PrintDebugMessage();
        }
        
    }

    private void PrintDebugMessage()
    {
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine(">>-- Biomes & Locations --<<");
        foreach (var biome in _biomes)
        {
            Console.WriteLine("> " + biome.Name);
            foreach (var location in biome.LocationsList)
                Console.WriteLine("  > " + location.Name);
        }
        Console.WriteLine("");
        Console.WriteLine(">>-- Locations & Quests --<<");
        foreach (var location in _locations)
        {
            Console.WriteLine("> " + location.Name);
            if (location.Quest != null)
            {
                Console.WriteLine("  > " + location.Quest.Name);
            }
        }
        Console.WriteLine("");
        Console.WriteLine(">>-- Locations Randomized Linking --<<");
        foreach (var location in _locations)
        {
            Console.WriteLine("> " + location.Name);
            if (location.UpLocation != null)
            {
                Console.WriteLine("  > [UP] " + location.UpLocation.Name);
            }
            if (location.DownLocation != null)
            {
                Console.WriteLine("  > [DOWN] " + location.DownLocation.Name);
            }
            if (location.LeftLocation != null)
            {
                Console.WriteLine("  > [LEFT] " + location.LeftLocation.Name);
            }
            if (location.RightLocation != null)
            {
                Console.WriteLine("  > [RIGHT] " + location.RightLocation.Name);
            }
        }
    }

    private void LinkContent()
    {
        var rnd = new Random();

        foreach (var location in _locations)
        {
            var biomeId = _biomes.FindIndex(biome => biome.Name == location.Biome.Name);
            _biomes[biomeId].LocationsList.Add(location);
        }

        for (var locationIndex = 0; locationIndex < _locations.Count; locationIndex++)
        {
            var location = _locations[locationIndex];
            if (_locations.Count - locationIndex - 1 < 1)
            {
                continue;
            }
            
            int numberOfExits;
            if (_locations.Count - locationIndex - 1 < 2)
                numberOfExits = 1;
            else
                numberOfExits = rnd.Next(1, 3);
            
            int enteredAt;
            if (location.DownLocation != null)
            {
                enteredAt = 1;
            }
            else if (location.LeftLocation != null)
            {
                enteredAt = 2;
            }
            else if (location.UpLocation != null)
            {
                enteredAt = 3;
            }
            else if (location.RightLocation != null)
            {
                enteredAt = 4;
            }
            else
            {
                enteredAt = 1;
            }
            
            if (numberOfExits == 2)
            {
                int exit1, exit2;
                var exitsChoice = rnd.Next(1, 4);
                var nextLocationChoice = rnd.Next(1, 3);
                switch (exitsChoice)
                {
                    case 1:
                        exit1 = Math.Abs(enteredAt + 2) % 4 + 1;
                        exit2 = Math.Abs(enteredAt + 1) % 4 + 1;
                        break;
                    case 2:
                        exit1 = Math.Abs(enteredAt - 2) % 4 + 1;
                        exit2 = Math.Abs(enteredAt + 2) % 4 + 1;
                        break;
                    default:
                        exit1 = Math.Abs(enteredAt + 1) % 4 + 1;
                        exit2 = Math.Abs(enteredAt - 2) % 4 + 1;
                        break;
                }
                if (nextLocationChoice == 2)
                {
                    LinkLocations(location, _locations[locationIndex+2], exit1);
                    LinkLocations(location, _locations[locationIndex+1], exit2);
                    locationIndex++;
                }
                else
                {
                    LinkLocations(location, _locations[locationIndex+2], exit1);
                    LinkLocations(location, _locations[locationIndex+1], exit2);
                    locationIndex++;

                }
            }
            else
            {
                var exitsChoice = rnd.Next(1, 3);
                int exit;
                if (exitsChoice == 1)
                {
                    exit = Math.Abs(enteredAt + 1) % 4 + 1;
                }
                else if (exitsChoice == 2)
                {
                    exit = Math.Abs(enteredAt + 2) % 4 + 1;
                }
                else
                {
                    exit = Math.Abs(enteredAt + 3) % 4 + 1;
                }
                LinkLocations(location, _locations[locationIndex+1], exit);
            }
        }

        for (var biomeIndex = 0; biomeIndex < _biomes.Count; biomeIndex++)
        {
            var biome = _biomes[biomeIndex];
            var biomeQuests = _quests.Where(quest => quest.BiomeId == biomeIndex + 1).ToList();
            biomeQuests = biomeQuests.OrderBy(_ => rnd.Next()).ToList();
            for (var locationIndex = 0; locationIndex < biome.LocationsList.Count; locationIndex++)
            {
                var biomeLocation = biome.LocationsList[locationIndex];
                biomeLocation.Quest = biomeQuests[locationIndex];
            }
        }
    }
    
    private void LinkLocations(Location loc1, Location loc2, int direction)
    { 
        switch (direction)
        {
            case 3:
                loc1.UpLocation = loc2;
                loc2.DownLocation = loc1;
                break;
            case 1:
                loc1.DownLocation = loc2;
                loc2.UpLocation = loc1;
                break;
            case 2:
                loc1.LeftLocation = loc2;
                loc2.RightLocation = loc1;
                break;
            case 4:
                loc1.RightLocation = loc2;
                loc2.LeftLocation = loc1;
                break;
        }
    }

    public Location GetStartingLocation()
    {
        return _locations[0];
    }

    public (int, int) GetMaxProgressState()
    {
        return (_locations.Count, _quests.Count);
    }

    private void GenerateQuests()
    {
        using var reader = new StreamReader("../../../data/quests.csv");
        int readQuest = 0;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null)
            {
                break;
            }
            if (line.StartsWith('#'))
            {
                continue;
            }
            if (line.Length == 0)
            {
                continue;
            }
            var values = line.Split('|');
            string[] dialogs = { values[2], values[3], values[4] };
            var questType = values[9] == "REGULAR" ? QuestType.Regular : QuestType.NpcQuest;
            _quests.Add(new Quest(values[0],values[1], dialogs, int.Parse(values[5]), values[6], values[7], int.Parse(values[8]), questType));
            readQuest++;
        }

        if (readQuest == 0)
        {
            Console.WriteLine("[ERROR] No Quest loaded (are you missing the quests.txt file in the data directory?).");
            Console.WriteLine("\n\n\n");
        }
        
    }

    private void GenerateLocations()
    {
        using var reader = new StreamReader("../../../data/locations.csv");
        var locationIndexCounter = 0;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null)
            {
                break;
            }
            if (line.StartsWith('#'))
            {
                continue;
            }
            if (line.Length == 0)
            {
                continue;
            }
            var values = line.Split('|');
            var extractedBiomeId = int.Parse(values[2]) - 1;
            if (extractedBiomeId < 0 || extractedBiomeId >= _biomes.Count) continue;
            _locations.Add(new Location(locationIndexCounter, values[0], values[1], _biomes[extractedBiomeId]));
            locationIndexCounter++;
        }
        if (locationIndexCounter == 0)
        {
            Console.WriteLine("[ERROR] No Locations loaded (are you missing the locations.txt file in the data directory?).");
            Console.WriteLine("\n\n\n");

        }
    }

    private void GenerateBiomes()
    {
        _biomes.Add(new Biome("Biome 1", "Biome 1 Description", 0));
        _biomes.Add(new Biome("Biome 2", "Biome 2 Description", 25));
        _biomes.Add(new Biome("Biome 3", "Biome 3 Description", 150));
        _biomes.Add(new Biome("Biome 4", "Biome 4 Description", 537));
        _biomes.Add(new Biome("Biome 5", "Biome 5 Description", 1352));
    }
}