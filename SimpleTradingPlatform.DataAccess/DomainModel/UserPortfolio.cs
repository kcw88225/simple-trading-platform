using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradingPlatform.DataAccess.DomainModel
{
    public class UserPortfolio
    {
        public UserPortfolio()
        {
            Shares = new Dictionary<int, int>();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Cash { get; set; }

        public Dictionary<int, int> Shares { get; set; }
    }
}
