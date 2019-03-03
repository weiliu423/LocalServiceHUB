using ServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ServiceAPI.Interfaces
{
    public interface IinfoService
    {

        Task<IEnumerable<string>> getAllServiceSql();
        Task<bool> createService(infoModel data);
        Task<bool> addServiceType(ServiceType data);

    }
}