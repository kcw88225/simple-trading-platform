using SimpleTradingPlatform.DataAccess;
using SimpleTradingPlatform.DataAccess.Interface;
using SimpleTradingPlatform.Service;
using SimpleTradingPlatform.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SimpleTradingPlatform
{
    public static class UnityIocConfig
    {
        private static UnityContainer _iocContainer;

        public static void Setup()
        {
            _iocContainer = new UnityContainer();
            _iocContainer.RegisterType<IRepository, InMemoryRepository>();
            _iocContainer.RegisterType<IShareService, ShareService>();
            _iocContainer.RegisterType<IUserPortfolioService, UserPortfolioService>();
            _iocContainer.RegisterType<ITradingService, TradingService>();
        }

        public static UnityContainer Container
        {
            get
            {
                return _iocContainer;
            }
        }
    }
}
