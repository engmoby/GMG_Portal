﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GMG_Portal.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GMG_Portal_DBEntities1 : DbContext
    {
        public GMG_Portal_DBEntities1()
            : base("name=GMG_Portal_DBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Front_Mission> Front_Mission { get; set; }
        public virtual DbSet<Front_Mission_Translate> Front_Mission_Translate { get; set; }
        public virtual DbSet<Front_Vision> Front_Vision { get; set; }
        public virtual DbSet<Front_Vision_Translate> Front_Vision_Translate { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Hotels_Features> Hotels_Features { get; set; }
        public virtual DbSet<Hotels_Features_Translate> Hotels_Features_Translate { get; set; }
        public virtual DbSet<Hotels_Images> Hotels_Images { get; set; }
        public virtual DbSet<Hotels_Payments> Hotels_Payments { get; set; }
        public virtual DbSet<Hotels_Phones> Hotels_Phones { get; set; }
        public virtual DbSet<Hotels_Videos> Hotels_Videos { get; set; }
        public virtual DbSet<Hotels_Reservation> Hotels_Reservation { get; set; }
        public virtual DbSet<Hotels_Translate> Hotels_Translate { get; set; }
        public virtual DbSet<Hotles_Offers> Hotles_Offers { get; set; }
        public virtual DbSet<Hotles_Offers_Translate> Hotles_Offers_Translate { get; set; }
        public virtual DbSet<SystemParameters_About> SystemParameters_About { get; set; }
        public virtual DbSet<SystemParameters_About_Translate> SystemParameters_About_Translate { get; set; }
        public virtual DbSet<SystemParameters_CareerForm> SystemParameters_CareerForm { get; set; }
        public virtual DbSet<SystemParameters_Careers> SystemParameters_Careers { get; set; }
        public virtual DbSet<SystemParameters_Careers_Translate> SystemParameters_Careers_Translate { get; set; }
        public virtual DbSet<SystemParameters_Category> SystemParameters_Category { get; set; }
        public virtual DbSet<SystemParameters_Category_Translate> SystemParameters_Category_Translate { get; set; }
        public virtual DbSet<SystemParameters_Cities> SystemParameters_Cities { get; set; }
        public virtual DbSet<SystemParameters_ContactForm> SystemParameters_ContactForm { get; set; }
        public virtual DbSet<SystemParameters_ContactUs> SystemParameters_ContactUs { get; set; }
        public virtual DbSet<SystemParameters_ContactUs_Translate> SystemParameters_ContactUs_Translate { get; set; }
        public virtual DbSet<SystemParameters_CoreValues> SystemParameters_CoreValues { get; set; }
        public virtual DbSet<SystemParameters_CoreValues_Translate> SystemParameters_CoreValues_Translate { get; set; }
        public virtual DbSet<SystemParameters_Countries> SystemParameters_Countries { get; set; }
        public virtual DbSet<SystemParameters_Departments> SystemParameters_Departments { get; set; }
        public virtual DbSet<SystemParameters_Features> SystemParameters_Features { get; set; }
        public virtual DbSet<SystemParameters_Features_Translate> SystemParameters_Features_Translate { get; set; }
        public virtual DbSet<SystemParameters_HomeSlider> SystemParameters_HomeSlider { get; set; }
        public virtual DbSet<SystemParameters_HomeSlider_Translate> SystemParameters_HomeSlider_Translate { get; set; }
        public virtual DbSet<Systemparameters_Languages> Systemparameters_Languages { get; set; }
        public virtual DbSet<SystemParameters_LanguageText> SystemParameters_LanguageText { get; set; }
        public virtual DbSet<SystemParameters_News> SystemParameters_News { get; set; }
        public virtual DbSet<SystemParameters_News_Translate> SystemParameters_News_Translate { get; set; }
        public virtual DbSet<SystemParameters_Newsletter> SystemParameters_Newsletter { get; set; }
        public virtual DbSet<SystemParameters_Owners> SystemParameters_Owners { get; set; }
        public virtual DbSet<SystemParameters_Owners_Translate> SystemParameters_Owners_Translate { get; set; }
        public virtual DbSet<SystemParameters_PaymentType> SystemParameters_PaymentType { get; set; }
    }
}
