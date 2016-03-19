using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Store
{
    public  class TimeConvertController
    {
        public DateTime DateConvert(string date)
        {
            string dt = String.Format("{0}-{1}-{2}", date.Split('/')[2], date.Split('/')[1], date.Split('/')[0]);
            return Convert.ToDateTime(dt);
        }
    }
}
