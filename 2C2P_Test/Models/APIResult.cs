using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2C2P_Test.Models
{
    public class APIResult
    {
        public String Id { get; set; }
        public String Payment { get; set; }
        public String Status { get; set; }

        public String ErrorMessage { get; set; }
    }
}