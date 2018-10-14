using MVCHUB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCHUB.Services
{
    public class DBContext : IDBContext
    {
        accountModelEF Context = new accountModelEF();
       
        public DbSet<Account> Account {
            get
            {
                return Context.Accounts;
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public accountModelEF Contexts
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

        accountModelEF IDBContext.Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}