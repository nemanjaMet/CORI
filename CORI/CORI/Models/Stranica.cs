using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CORI.Models
{
    public class Stranica
    {
        public string ime { get; set; }
        public string html { get; set; }
    }

    public class User
    {
        public string userName { get; set; }
        public List<Stranica> pageList { get; set; }
    }
}