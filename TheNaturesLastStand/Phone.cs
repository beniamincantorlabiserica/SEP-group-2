
namespace TheNaturesLastStand
{
    public class Phone : Item
    {
        public void CallFireFighters()
        {

        }

        public void Call911()
        {
        }

        public override void Use()
        {
            throw new NotImplementedException();   //automatic, just to have something in the method
        }

        public override void Pick()
        {
            throw new NotImplementedException();
        }

        public override void Drop(int locationId)
        {
            throw new NotImplementedException();
        }
    }

}
