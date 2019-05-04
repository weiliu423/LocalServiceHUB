using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceAPI.Models
{
    public class ServiceInfoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public DateTime CreateDate { get; set; }
        public string ServiceLocation { get; set; }
    }
}