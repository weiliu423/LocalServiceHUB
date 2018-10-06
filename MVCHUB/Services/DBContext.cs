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
        Login Context = new Login();
        public DbSet<UserAccount> UserAccount {
            get
            {
                return Context.UserAccounts;
            }

            set
            {
                throw new NotImplementedException();
            }
        }
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
        public Login Contexts
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

        Login IDBContext.Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}