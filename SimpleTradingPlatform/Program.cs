using SimpleTradingPlatform.DataAccess;
using SimpleTradingPlatform.DataAccess.DomainModel;
using SimpleTradingPlatform.Service;
using SimpleTradingPlatform.Service.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradingPlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new InMemoryRepository();
            var shareService = new ShareService(repository);
            var userPortfolioService = new UserPortfolioService(repository);
            var tradingService = new TradingService(userPortfolioService, shareService);

            var userPortfolioId = 1;
            var shareId = 1;
            var userPortfolio = userPortfolioService.Get(1);


            Console.WriteLine("* Start with a user with no share on hand:");
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to buy 1 share of Share A:");
            userPortfolio = tradingService.BuyShare(userPortfolioId, shareId, 1);
            var temp = userPortfolioService.Get(1);
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to sell 1 share of Share A:");
            userPortfolio = tradingService.SellShare(userPortfolioId, shareId, 1);
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to buy 100 shares when the user don't have enough cash:");
            try
            {
                userPortfolio = tradingService.BuyShare(userPortfolioId, shareId, 100);
            }
            catch(NotEnoughCashException)
            {
                Console.WriteLine("Not enough cash!");
            }
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to sell 100 shares when the user don't have enough shares:");
            try
            {
                userPortfolio = tradingService.SellShare(userPortfolioId, shareId, 100);
            }
            catch (NotEnoughUserShareException)
            {
                Console.WriteLine("Not enough shares!");
            }
            ShowPortfolio(userPortfolio);

            Console.ReadLine();
        }


        private static void ShowPortfolio(UserPortfolio userPortfolio)
        {
            //TODO => IOC
            var repository = new InMemoryRepository();
            var shareService = new ShareService(repository);

            Console.WriteLine("Name: " + userPortfolio.Name);
            Console.WriteLine("Remaining amount of cash: " + userPortfolio.Cash);

            if (!userPortfolio.Shares.Any())
            {
                Console.WriteLine("This user does not have any shares.");
            }
            else
            {
                foreach (var shareRecord in userPortfolio.Shares)
                {
                    var share = shareService.Get(shareRecord.Key);
                    Console.WriteLine("Share name: " + share.Name);
                    Console.WriteLine("Share price: " + share.Price);
                    Console.WriteLine("Quantity: " + shareRecord.Value);
                    Console.WriteLine("Price total: " + (shareRecord.Value * share.Price));
                }
            }
            Console.WriteLine("=================================");
        }
    }
}
