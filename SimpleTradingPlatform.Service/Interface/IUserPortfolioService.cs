using SimpleTradingPlatform.DataAccess.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradingPlatform.Service.Interface
{
    public interface IUserPortfolioService
    {
        List<UserPortfolio> GetAll();

        UserPortfolio Get(int id);

        bool Update(UserPortfolio userPortfolio);
    }
}
