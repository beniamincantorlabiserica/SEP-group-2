
namespace TheNaturesLastStand;

public class Item
{
	private int quest_id;

	public int Quest_ID
	{
		get { return quest_id; }
		set { quest_id = value; }
	}

	private int biome_id;

	public int Biome_ID
	{
		get { return biome_id; }
		set { biome_id = value; }
	}


	private string name;

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	private string description;

	public string Description
	{
		get { return description; }
		set { description = value; }
	}

    /// <summary>
    /// Constructor of class Item Initializing the attirbutes
    /// </summary>
    /// <param name="Quest_ID">what quest the item is tied to</param>
    /// <param name="Name">item name</param>
    /// <param name="Description">item description</param>
    /// <param name="Biome_ID">what biome the item is in</param>
	public Item(string Name, string Description, int Quest_ID, int Biome_ID)
	{
		this.Quest_ID = Quest_ID;
		this.Name = Name;
		this.Description = Description;
		this.Biome_ID = Biome_ID;
	}
}