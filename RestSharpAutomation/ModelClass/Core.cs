using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.ModelClass
{
    public class Core
    {
        public string currencyIsoCode { get; set; }
        public bool isExternalAccount { get; set; }
        public bool isExternalBalance { get; set; }
        public int registeredProductId { get; set; }
        public int userTypeId { get; set; }
        public int userId { get; set; }
        public string username { get; set; }
    }
}
