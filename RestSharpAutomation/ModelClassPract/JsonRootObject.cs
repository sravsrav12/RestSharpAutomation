using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation
{
    
        public class JsonRootObject
        {
            public Location location { get; set; }
            public int accuracy { get; set; }
            public string name { get; set; }
            public string phone_number { get; set; }
            public string address { get; set; }
            // public List<string> types { get; set; }
            public string website { get; set; }
            public string language { get; set; }
        }
    }

