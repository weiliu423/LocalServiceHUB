using ServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ServiceAPI.Interfaces
{
    public interface IServicesService
    {
        //Task<IEnumerable<string>> getAllUsers();
        Task<IEnumerable<string>> getAllServiceSql();

        //Task<AccountModel> createNewAccount(AccountModel data);

    }
}