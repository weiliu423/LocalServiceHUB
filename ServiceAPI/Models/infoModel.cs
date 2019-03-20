using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceAPI.Models
{
    public class infoModel
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public int LinkAccountId { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
    }
}