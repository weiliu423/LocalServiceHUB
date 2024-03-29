﻿using MVCHUB.Models;
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
        Task<IEnumerable<string>> getAllUsersSql();
        Task<AccountModel> createNewAccount(AccountModel data);
        Task<String> accountValidation(AccountModel data);
        Task<bool> DeleteAccount(int id);
        Task<IEnumerable<string>> getUserByName(String name);
    }
}