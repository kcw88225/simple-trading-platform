using SimpleTradingPlatform.DataAccess.DomainModel;
using SimpleTradingPlatform.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradingPlatform.DataAccess
{
    public class InMemoryRepository : IRepository
    {
        private IEnumerable<Share> _share = new List<Share>() { 
            new Share() { ID = 1, Name = "Share A", Price = 10 },
            new Share() { ID = 2, Name = "Share B", Price = 20 },
            new Share() { ID = 3, Name = "Share C", Price = 30 },
            new Share() { ID = 4, Name = "Share D", Price = 40 },
            new Share() { ID = 5, Name = "Share E", Price = 50 }
        };

        private IEnumerable<UserPortfolio> _userPortfolios = new List<UserPortfolio>() { 
            new UserPortfolio() { ID = 1, Name = "User A", Cash = 30 }
        };

        public IEnumerable<Share> Shares()
        {
            return _share;
        }

        public IEnumerable<UserPortfolio> UserPortfolios()
        {
            return _userPortfolios;
        }
    }
}
