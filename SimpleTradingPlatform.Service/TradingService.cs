using SimpleTradingPlatform.DataAccess.DomainModel;
using SimpleTradingPlatform.DataAccess.Interface;
using SimpleTradingPlatform.Service.Exception;
using SimpleTradingPlatform.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradingPlatform.Service
{
    public class TradingService : ITradingService
    {
        private readonly IUserPortfolioService _userPortfolioService;
        private readonly IShareService _shareService;

        public TradingService(IUserPortfolioService userPortfolioService, IShareService shareService)
        {
            _userPortfolioService = userPortfolioService;
            _shareService = shareService;
        }

        public UserPortfolio BuyShare(int portfolioId, int shareId, int quantity)
        {
            var userPortfolio = _userPortfolioService.Get(portfolioId);
            var share = _shareService.Get(shareId);

            var totalPrice = share.Price * quantity;
            if(userPortfolio.Cash < totalPrice)
            {
                throw new NotEnoughCashException();
            }

            if (userPortfolio.Shares.ContainsKey(shareId))
                userPortfolio.Shares[shareId] += quantity;
            else
                userPortfolio.Shares.Add(shareId, quantity);

            userPortfolio.Cash -= totalPrice;
            _userPortfolioService.Update(userPortfolio);

            return userPortfolio;
        }

        public UserPortfolio SellShare(int portfolioId, int shareId, int quantity)
        {
            var userPortfolio = _userPortfolioService.Get(portfolioId);
            var share = _shareService.Get(shareId);

            var holdingShareCount = userPortfolio.Shares.ContainsKey(shareId) ? userPortfolio.Shares[shareId] : 0;
            if (holdingShareCount < quantity)
            {
                throw new NotEnoughUserShareException();
            }

            var totalPrice = share.Price * quantity;
            userPortfolio.Shares[shareId] -= quantity;
            userPortfolio.Cash += totalPrice;
            _userPortfolioService.Update(userPortfolio);

            return userPortfolio;
        }
    }
}
