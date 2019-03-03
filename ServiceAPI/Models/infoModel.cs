using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceAPI.Models
{
    public class infoModel
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int LinkAccountId { get; set; }
        public string Description { get; set; }
    }
}