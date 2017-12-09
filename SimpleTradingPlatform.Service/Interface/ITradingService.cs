using SimpleTradingPlatform.DataAccess.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradingPlatform.Service.Interface
{
    public interface ITradingService
    {
        UserPortfolio BuyShare(int portfolioId, int shareId, int quantity);

        UserPortfolio SellShare(int portfolioId, int shareId, int quantity);
    }
}
