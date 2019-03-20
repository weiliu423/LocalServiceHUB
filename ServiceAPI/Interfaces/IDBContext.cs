using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServiceAPI.Interfaces
{
    public interface IDBContext
    {
        LSHUBEntities Context { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<ServiceType> ServiceType { get; set; }
    }
}