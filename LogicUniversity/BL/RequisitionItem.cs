//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BL
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequisitionItem
    {
        public string Requisition_ID { get; set; }
        public string Item_ID { get; set; }
        public int Required_Qty { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Requisition Requisition { get; set; }
    }
}
