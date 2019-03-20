using ServiceAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServiceAPI.Services
{
    public class DBContext : IDBContext
    {
        LSHUBEntities Context = new LSHUBEntities();
       
        public DbSet<Service> Services
        {
            get
            {
                return Context.Services;
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public DbSet<ServiceType> ServiceType
        {
            get
            {
                return Context.ServiceTypes;
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public LSHUBEntities Contexts
        {
            get
            {
                return Context;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        LSHUBEntities IDBContext.Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}