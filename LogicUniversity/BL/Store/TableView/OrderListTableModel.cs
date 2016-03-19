using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store.TableView
{
  public  class OrderListTableModel
    {

        public string Order_ID { get; set; }
        public string Supplier_Name { get; set; }
        public DateTime Order_Date { get; set; }

        public Nullable <DateTime> Received_Date { get; set; }
        public string Rec_Staff { get; set; }
        public string Ord_Staff { get; set; }

        public string Rec_Staff_id { get; set; }
        public string Ord_Staff_id { get; set; }
        public string Status { get; set; }

       
       

        
    }
}
