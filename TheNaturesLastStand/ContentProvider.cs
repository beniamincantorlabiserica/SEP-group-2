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
        using var reader = new StreamReader("./data/quests.csv");
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
            string[] dialogs = { values[3], values[4], values[5] };
            QuestType CurrentQuestType;
            switch(values[10])
            {
                case "REGULAR":
                    CurrentQuestType = QuestType.Regular;
                    break;
                case "NPC":
                    CurrentQuestType = QuestType.NpcQuest;
                    break;
                case "ITEM":
                    CurrentQuestType = QuestType.ItemQuest;
                    break;
                default:
                    CurrentQuestType = QuestType.Regular;
                    break;
            }
            _quests.Add(new Quest(int.Parse(values[0]), values[1], values[2], dialogs, int.Parse(values[6]), values[7], values[8], int.Parse(values[9]), CurrentQuestType));
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
        using var reader = new StreamReader("./data/locations.csv");
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

        GenerateItems();
    }
    private void GenerateItems()
    {
        List<Location> ScrambledLocations = _locations.OrderBy(x => Random.Shared.Next()).ToList();
        Random rnd = new Random();
        using (StreamReader reader = new StreamReader("./data/Items.csv"))
        {
            while (!reader.EndOfStream) 
            {
                string line = reader.ReadLine();
                string[] arguments = line.Split('|');

                foreach (Location location in ScrambledLocations)
                {
                    if(location.Item == null && location.Biome.ID == int.Parse(arguments[3]))
                    {
                        location.Item = new Item(arguments[0], arguments[1], int.Parse(arguments[2]), int.Parse(arguments[3]));
                        break;
                    }
                }   
            }
        }
    }

    private void GenerateBiomes()
    {
        _biomes.Add(new Biome(1, "Seaside", "Serenity unfolds on a pristine island seaside paradise.", 0));
        _biomes.Add(new Biome(2, "Ocean", "Azure expanse, rhythmic waves, endless horizon's tranquil beauty.", 25));
        _biomes.Add(new Biome(3, "Rain Forest", "Lush green canopy, vibrant life, nature's symphony thrives.", 55));
        _biomes.Add(new Biome(4, "Savana", "Golden grasslands, majestic wildlife, endless horizons, untamed beauty.", 80));
        _biomes.Add(new Biome(5, "Desert", "Sculpted dunes, golden silence, desert blooms in solitude.", 150));
    }
}