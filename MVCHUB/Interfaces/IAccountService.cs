using MVCHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCHUB.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<string>> getAllUsers();
        Task<AccountModel> createNewAccount(AccountModel data);
    }
}