using SimpleTradingPlatform.DataAccess.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradingPlatform.DataAccess.Interface
{
    public interface IRepository
    {
        IEnumerable<Share> Shares();

        IEnumerable<UserPortfolio> UserPortfolios();
    }
}
