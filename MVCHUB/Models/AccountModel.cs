using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHUB.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string EncPass { get; set; }
        public string ResourceKey { get; set; }
       
    }
}