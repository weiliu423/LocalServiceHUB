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

        Task<List<ServiceDataModel>> getAllServiceSql(string categoryName);
        Task<bool> createService(infoModel data);
        Task<bool> addServiceType(ServiceType data);
        Task<IEnumerable<string>> getAllCategories();
        Task<List<ServiceDataModel>> getAllServices();
        Task<List<ServiceDataModel>> getAllServiceByAccount(string accountName);
        Task<bool> updateService(infoModel data);

    }
}