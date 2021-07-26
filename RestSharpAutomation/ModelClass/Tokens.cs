using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.ModelClass
{
    public class Tokens
    {
        public int userTokenExpiryInSeconds { get; set; }
        public List<string> launchTokens { get; set; }
        public List<string> refreshTokens { get; set; }
        public string userToken { get; set; }
    }
}
