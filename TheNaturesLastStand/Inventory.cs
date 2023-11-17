public class Inventory
{
    private Item_Lasting Empty_Item_Slot;
    private Item[] storage;

    public Item[] Storage
    {
        get { return storage; }
    }

    public Inventory(int Item_Capacity)
    {
        storage = new Item[Item_Capacity];
        Empty_Item_Slot = new Item_Lasting(0, "Empty item slot", "Empty item slot");
        Init_Storage_Array();
    }

    private void Init_Storage_Array()
    {
        //Fills up the storage array with the items that function as "empty" slots
        for (int i = 0; i < Storage.Length; i++)
        {
            Storage[i] = Empty_Item_Slot;
        }
    }

    private int Has_Empty_Slot()
    {
        //returns an empty slot index in the storage array
        //if the bag is full it returns -1

        for (int i = 0; i < storage.Length; i++)
        {
            if (storage[i] == Empty_Item_Slot)
            {
                return i;
            }
        }

        return -1;
    }

    private bool Is_Empty()
    {
        //checks if the storage array is empty
        int counter = 0;
        for (int i = 0; i < storage.Length; i++)
        {
            if (storage[i] == Empty_Item_Slot)
            {
                return false;
            }
        }

        return true;
    }

    private int Find_Item_From_ID(int ID)
    {
        //returns the index of the item in the storage array with the specified ID
        //returns -1 if it does not exist
        for (int i = 0; i < storage.Length; i++)
        {
            if (storage[i].ID == ID)
            {
                return i;
            }
        }

        return -1;
    }

    public bool Add(Item New_Item)
    {
        //Adds the specified item to the storage array
        //returns -1 if the storage array has no empty slots
        int Empty_Slot = Has_Empty_Slot();

        if (Empty_Slot != -1)
        {
            storage[Empty_Slot] = New_Item;
            return true;
        }

        return false;
    }

    public bool Remove(int ID)
    {
        //removes the item with the specified ID from the storage array
        //returns false if the storage array is empty or if the item can not be found
        //returns true if the opposite is true
        if (Is_Empty())
        {
            int Item_Index = Find_Item_From_ID(ID);
            if (Item_Index != -1)
            {
                Item dropped_item = storage[Item_Index];
                storage[Item_Index] = Empty_Item_Slot;
                return true;
            }
        }

        return false;
    }

    public Item Drop(int ID)
    {
        //returns an item from the storage array with the specified ID
        //returns the empty_item_slot if the bag is empty
        if (Is_Empty())
        {
            int Item_Index = Find_Item_From_ID(ID);
            if (Item_Index != -1)
            {
                Item dropped_item = storage[Item_Index];
                storage[Item_Index] = Empty_Item_Slot;
                return dropped_item;
            }
        }

        return Empty_Item_Slot;
    }
}
