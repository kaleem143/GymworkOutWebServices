﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GymBuddy.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GymBuddyDb : DbContext
    {
        public GymBuddyDb()
            : base("name=GymBuddyDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblNutritionPlan> tblNutritionPlans { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblWorkOutPlan> tblWorkOutPlans { get; set; }
    
        public virtual int pro_approveClient1(Nullable<int> clientID, Nullable<int> requestStatus, ObjectParameter username)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("clientID", clientID) :
                new ObjectParameter("clientID", typeof(int));
    
            var requestStatusParameter = requestStatus.HasValue ?
                new ObjectParameter("requestStatus", requestStatus) :
                new ObjectParameter("requestStatus", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pro_approveClient1", clientIDParameter, requestStatusParameter, username);
        }
    }
}
