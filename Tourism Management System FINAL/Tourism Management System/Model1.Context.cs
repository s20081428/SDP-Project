﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class project_dbEntities : DbContext
    {
        public project_dbEntities()
            : base("name=project_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<aircontry> aircontries { get; set; }
        public DbSet<airline> airlines { get; set; }
        public DbSet<attractbooking> attractbookings { get; set; }
        public DbSet<attraction> attractions { get; set; }
        public DbSet<cruise> cruises { get; set; }
        public DbSet<cruisebooking> cruisebookings { get; set; }
        public DbSet<cruiseorganizer> cruiseorganizers { get; set; }
        public DbSet<customer> customers { get; set; }
        public DbSet<driver> drivers { get; set; }
        public DbSet<driverroster> driverrosters { get; set; }
        public DbSet<equipment> equipments { get; set; }
        public DbSet<equipmentlist> equipmentlists { get; set; }
        public DbSet<flightbooking> flightbookings { get; set; }
        public DbSet<flightclass> flightclasses { get; set; }
        public DbSet<flightschedule> flightschedules { get; set; }
        public DbSet<hotel> hotels { get; set; }
        public DbSet<hotelbooking> hotelbookings { get; set; }
        public DbSet<room> rooms { get; set; }
        public DbSet<staff> staffs { get; set; }
        public DbSet<vehicle> vehicles { get; set; }
        public DbSet<vehiclebooking> vehiclebookings { get; set; }
        public DbSet<attractprice> attractprices { get; set; }
    }
}