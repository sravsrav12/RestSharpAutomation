using RestSharpAutomation.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation
{
    public class JsonRoot
    {
        public Environment environment { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string sessionProductId { get; set; }
        public int numLaunchTokens { get; set; }
        public string marketType { get; set; }
        public Tokens tokens { get; set; }
    }
}
