using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    public class ItemInfobyReqiIdModel_M
    {
        string RequisitionId;

        public string RequisitionId1
        {
            get { return RequisitionId; }
            set { RequisitionId = value; }
        }
        string ItemCode;

        public string ItemCode1
        {
            get { return ItemCode; }
            set { ItemCode = value; }
        }
        int itemQty;

        public int ItemQty
        {
            get { return itemQty; }
            set { itemQty = value; }
        }
        int Item_assigned;

        public int Item_assigned1
        {
            get { return Item_assigned; }
            set { Item_assigned = value; }
        }

    }
}
