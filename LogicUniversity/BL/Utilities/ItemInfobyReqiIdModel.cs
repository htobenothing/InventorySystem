using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public  class ItemInfobyReqiIdModel
    {
        string ReuisitionId;
        string ItemCode;
        int itemQty;
        int Item_assigned;

        public string ReuisitionId1
        {
            get { return ReuisitionId; }
            set { ReuisitionId = value; }
        }
       
        public string ItemCode1
        {
            get { return ItemCode; }
            set { ItemCode = value; }
        }

        public int ItemQty
        {
            get { return itemQty; }
            set { itemQty = value; }
        }


        public int Item_assigned1
        {
            get { return Item_assigned; }
            set { Item_assigned = value; }
        }
    }
}
