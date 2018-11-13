using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHUB.Models
{
    public class ScoreModel
    {
      
            public string userId { get; set; }
            public int played { get; set; }
            public int won { get; set; }
            public int lost { get; set; }

    }
}