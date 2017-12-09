using SimpleTradingPlatform.DataAccess;
using SimpleTradingPlatform.DataAccess.DomainModel;
using SimpleTradingPlatform.DataAccess.Interface;
using SimpleTradingPlatform.Service;
using SimpleTradingPlatform.Service.Exception;
using SimpleTradingPlatform.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SimpleTradingPlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            UnityIocConfig.Setup();
            var container = UnityIocConfig.Container;
            var repository = container.Resolve<IRepository>();
            var shareService = container.Resolve<IShareService>();
            var userPortfolioService = container.Resolve<IUserPortfolioService>();
            var tradingService = container.Resolve<ITradingService>();

            //Show Case (run this application for demo purpose)
            var userPortfolioId = 1;
            var shareAId = 1;
            var shareBId = 2;
            var userPortfolio = userPortfolioService.Get(userPortfolioId);

            Console.WriteLine("* Start with a user with no share on hand:");
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to buy 3 share of Share A:");
            userPortfolio = tradingService.BuyShare(userPortfolioId, shareAId, 3);
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to sell 2 share of Share A:");
            userPortfolio = tradingService.SellShare(userPortfolioId, shareAId, 2);
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to buy 1 share of Share B:");
            userPortfolio = tradingService.BuyShare(userPortfolioId, shareBId, 1);
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to buy 100 shares when the user does't have enough cash:");
            try
            {
                userPortfolio = tradingService.BuyShare(userPortfolioId, shareAId, 100);
            }
            catch(NotEnoughCashException)
            {
                Console.WriteLine("Not enough cash!");
            }
            ShowPortfolio(userPortfolio);

            Console.WriteLine("* Try to sell 100 shares when the user does't have enough shares:");
            try
            {
                userPortfolio = tradingService.SellShare(userPortfolioId, shareAId, 100);
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
            var container = UnityIocConfig.Container;
            var shareService = container.Resolve<IShareService>();

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
                    Console.WriteLine("- Share name: " + share.Name);
                    Console.WriteLine("Share price: " + share.Price);
                    Console.WriteLine("Quantity: " + shareRecord.Value);
                    Console.WriteLine("Share price total: " + (shareRecord.Value * share.Price));
                }
            }
            Console.WriteLine("=================================");
        }
    }
}
