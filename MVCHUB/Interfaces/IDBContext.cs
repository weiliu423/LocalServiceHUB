using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCHUB.Interfaces
{
    public interface IDBContext
    {
        Login Context { get; set; }
        DbSet<UserAccount> UserAccount { get; set; }
        DbSet<Account> Account { get; set; }
    

    }
}