using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleTradingPlatform.Service.Interface;
using SimpleTradingPlatform.DataAccess.DomainModel;
using SimpleTradingPlatform.DataAccess;
using Moq;
using SimpleTradingPlatform.DataAccess.Interface;
using System.Collections.Generic;
using SimpleTradingPlatform.Service.Exception;

namespace SimpleTradingPlatform.Service.UnitTest
{
    [TestClass]
    public class TradingServiceTest
    {
        private readonly TradingService _tradingService;
        private readonly Mock<IShareService> _mockShareService;
        private readonly Mock<IUserPortfolioService> _mockUserPortfolioService;
        private const int _mockShareId = 1;
        private const int _mockUserPortfolioId = 1;

        public TradingServiceTest()
        {
            _mockShareService = new Mock<IShareService>();
            _mockShareService.Setup(m => m.Get(_mockShareId)).Returns(new Share() { ID = _mockShareId, Name = "Share A", Price = 10 });

            _mockUserPortfolioService = new Mock<IUserPortfolioService>();
            _mockUserPortfolioService.Setup(m => m.Get(_mockUserPortfolioId)).Returns(new UserPortfolio() { 
                ID = _mockUserPortfolioId, 
                Name = "User A", 
                Cash = 20
            });

            _tradingService = new TradingService(_mockUserPortfolioService.Object, _mockShareService.Object);
        }

        #region Test BuyShare
        [TestMethod]
        public void WhenBuyShare_GivenValidShareId_And_ValidPortfolioId_ShouldPortfolioUpdateShares_And_DeductCash()
        {
            //setup
            var userPortfolioId = _mockUserPortfolioId;
            var shareId = _mockShareId;
            var quantity = 2;

            //execise
            var result = _tradingService.BuyShare(userPortfolioId, shareId, quantity);

            //verify
            var expectedShareCount = 2;
            var expectedCash = 0;
            Assert.AreEqual(expectedShareCount, result.Shares[shareId]);
            Assert.AreEqual(expectedCash, result.Cash);
            _mockUserPortfolioService.Verify(m => m.Update(It.IsAny<UserPortfolio>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughCashException))]
        public void WhenBuyShare_GivenNotEnoughCashPortfolio_ShouldThrowNotEnoughCashException()
        {
            var userPortfolioId = _mockUserPortfolioId;
            var shareId = _mockShareId;
            var quantity = 100;

            var reuslt = _tradingService.BuyShare(userPortfolioId, shareId, quantity);
        }
        #endregion

        #region Test SellShare
        [TestMethod]
        public void WhenSellShare_GivenEnoughHoldingShares_ShouldPortfolioUpdateShares_And_AddCash()
        {
            var userPortfolioId = _mockUserPortfolioId;
            var shareId = _mockShareId;
            var quantity = 2;
            _mockUserPortfolioService.Setup(m => m.Get(_mockUserPortfolioId)).Returns(new UserPortfolio() {
                Cash = 20,
                Shares = new Dictionary<int, int>() { { _mockShareId, 2 } }
            });

            var result = _tradingService.SellShare(userPortfolioId, shareId, quantity);

            var expectedShareCount = 0;
            var expectedCash = 40;
            Assert.AreEqual(expectedShareCount, result.Shares[shareId]);
            Assert.AreEqual(expectedCash, result.Cash);
            _mockUserPortfolioService.Verify(m => m.Update(It.IsAny<UserPortfolio>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughUserShareException))]
        public void WhenSellShare_GivenNotEnoughUserSharePortfolio_ShouldThrowNotEnoughUserShareException()
        {
            var userPortfolioId = _mockUserPortfolioId;
            var shareId = _mockShareId;
            var quantity = 100;

            var reuslt = _tradingService.SellShare(userPortfolioId, shareId, quantity);
        }
        #endregion
    }
}
