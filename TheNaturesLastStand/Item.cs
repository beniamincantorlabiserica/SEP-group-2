public abstract class Item
{
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

    private int id;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public abstract void Use();

}

public class Item_Lasting : Item
{
    public Item_Lasting(int ID)
    {
        this.ID = ID;
        this.Name = "No name";
        this.Description = "No description";
    }

    public Item_Lasting(int ID, string Name, string Description)
    {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
    }

    public override void Use()
    {

    }
}

public class Item_Stackable : Item
{
    private int count;

    public int Count
    {
        get { return count; }
        set { count = value; }
    }


    public Item_Stackable(int ID)
    {
        this.ID = ID;
        this.Name = "No name";
        this.Description = "No description";
    }

    public Item_Stackable(int ID, string Name, string Description, int Count)
    {
        this.ID = ID;
        this.Name = Name;
        this.Description = Description;
        this.Count = Count;
    }

    public override void Use()
    {
        if (count - 1 >= 0)
        {
            count--;
        }
    }
}