using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceAPI.Models
{
    public class ServiceDataModel
    {
        public List<ServiceInfoModel> serviceInfo { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public int ContactNo
        {
            get; set;
        }
    }
}