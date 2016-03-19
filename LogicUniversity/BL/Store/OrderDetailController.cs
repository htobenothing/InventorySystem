using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store
{
    public class OrderDetailController
    {
        public string Item_ID;
        public int[] qtyCanSuppList;
        public string[] supplierList;

        public OrderDetailController(string itemID, string[] suppid, int[] qty)
        {
            this.Item_ID = itemID;
            qtyCanSuppList = new int[3];
            supplierList = new string[3];
            for (int i = 0; i < suppid.Count(); i++)
            {
                this.qtyCanSuppList[i] = qty[i];
                this.supplierList[i] = suppid[i];
            }
        }
    }
}
