using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNaturesLastStand
{
    abstract class Item
    {
        public abstract void Use();

        public abstract void Pick();

        public abstract void Drop(int locationId);
         
    }
    
}

