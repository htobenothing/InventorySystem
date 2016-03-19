using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Utilities
{
    class RetreivedItemModel
    {

        string itemcode;

        public string Itemcode
        {
            get { return itemcode; }
            set { itemcode = value; }
        }
        string desciption;

        public string Desciption
        {
            get { return desciption; }
            set { desciption = value; }
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
