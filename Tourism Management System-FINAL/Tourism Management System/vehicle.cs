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
    
    public partial class vehicle
    {
        public vehicle()
        {
            this.vehiclebookings = new HashSet<vehiclebooking>();
        }
    
        public string Vehicle_Name { get; set; }
        public decimal Price { get; set; }
        public string Vehicle_Type { get; set; }
        public int Capacity { get; set; }
        public string Gear { get; set; }
        public string Color { get; set; }
        public string Vehicle_ID { get; set; }
    
        public virtual ICollection<vehiclebooking> vehiclebookings { get; set; }
    }
}
