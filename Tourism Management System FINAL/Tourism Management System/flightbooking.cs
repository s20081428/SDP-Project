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
    
    public partial class flightbooking
    {
        public int BookingID { get; set; }
        public string FlightNo { get; set; }
        public System.DateTime DepDateTime { get; set; }
        public string Class { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string StaffID { get; set; }
        public string CustID { get; set; }
        public int AdultNum { get; set; }
        public int ChildNum { get; set; }
        public int InfantNum { get; set; }
        public int AdultPrice { get; set; }
        public int ChildPrice { get; set; }
        public int InfantPrice { get; set; }
        public decimal TotalAmt { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual flightclass flightclass { get; set; }
        public virtual staff staff { get; set; }
    }
}