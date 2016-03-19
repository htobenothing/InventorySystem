using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    public class RetreivedItemModel_M
    {
        string itemCode;

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        int neededQty;

        public int NeededQty
        {
            get { return neededQty; }
            set { neededQty = value; }
        }
        int providingQty;

        public int ProvidingQty
        {
            get { return providingQty; }
            set { providingQty = value; }
        }
    }
}
