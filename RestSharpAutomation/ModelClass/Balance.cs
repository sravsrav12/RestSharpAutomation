using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomation.ModelClass
{
    public class Balance
    {
        public double totalInAccountCurrency { get; set; }
        public List<Balance> balances { get; set; }
        public List<PointBalance> pointBalances { get; set; }
        public bool isBonusEnabled { get; set; }
        public bool isExternalBonusEnabled { get; set; }
    }
}
