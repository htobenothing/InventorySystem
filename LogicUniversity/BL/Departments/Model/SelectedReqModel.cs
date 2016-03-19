using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.Departments.Model
{

    public class SelectedReqModel
    {
        public string Requisition_ID { get; set; }

        public DateTime Submission_Date { get; set; }

        public string Staff_Name { get; set; }
        public string Type { get; set; }


    }
}
