using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Departments.Model
{
    public class SubmitItemDetailsModel
    {
        public string Category_ID { get; set; }
        public string Item_Name { get; set; }
        public int Required_Qty { get; set; }
        public string UOM { get; set; }
    }
}
