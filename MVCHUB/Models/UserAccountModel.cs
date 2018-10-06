using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHUB.Models
{
    public class UserAccountModel
    {
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}