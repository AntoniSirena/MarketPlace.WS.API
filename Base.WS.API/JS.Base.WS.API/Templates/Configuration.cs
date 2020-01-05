using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Templates
{
    public class Configuration
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public Enterprise Enterprise { get; set; }
    }

    public class Enterprise
    {
        public string Name { get; set; }
        public string RNC { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string EnterpriseType  { get; set; }
    }
}