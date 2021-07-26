using RestSharpAutomation.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation
{
    public class Packet
    {
        public int packetType { get; set; }
        public string payload { get; set; }
        public bool useFilter { get; set; }
        public bool isBase64Encoded { get; set; }

        public Balances balances { get; set; }
        public Financials financials { get; set; }
    }
}
