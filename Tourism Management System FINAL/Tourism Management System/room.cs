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
    
    public partial class room
    {
        public room()
        {
            this.hotelbookings = new HashSet<hotelbooking>();
        }
    
        public int HotelID { get; set; }
        public string RoomType { get; set; }
        public bool NonSmoking { get; set; }
        public int RoomNum { get; set; }
        public int RoomSize { get; set; }
        public int AdultNum { get; set; }
        public int ChildNum { get; set; }
        public string RoomDesc { get; set; }
        public int Price { get; set; }
    
        public virtual hotel hotel { get; set; }
        public virtual ICollection<hotelbooking> hotelbookings { get; set; }
    }
}
