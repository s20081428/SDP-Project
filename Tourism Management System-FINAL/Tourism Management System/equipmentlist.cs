//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tourism_Management_System
{
    using System;
    using System.Collections.Generic;
    
    public partial class equipmentlist
    {
        public int VehicleBookingID { get; set; }
        public string EquipID { get; set; }
        public decimal EquipBookPrice { get; set; }
        public int EquipBookingID { get; set; }
        public System.DateTime Orderdate { get; set; }
    
        public virtual vehiclebooking vehiclebooking { get; set; }
    }
}