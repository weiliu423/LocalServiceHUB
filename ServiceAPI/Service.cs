//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service
    {
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public int LinkAccountId { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
    
        public virtual ServiceType ServiceType { get; set; }
    }
}
