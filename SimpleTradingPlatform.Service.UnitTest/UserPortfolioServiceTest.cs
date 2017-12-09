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
    public class UserPortfolioServiceTest
    {
        private UserPortfolioService _userPortfolioService;
        private readonly Mock<IRepository> _mockRepository;
        private const int _validPortfolioId = 1;

        public UserPortfolioServiceTest()
        {
            _mockRepository = new Mock<IRepository>();
            _mockRepository.Setup(m => m.UserPortfolios()).Returns(new List<UserPortfolio> { 
                new UserPortfolio { ID = _validPortfolioId, Name = "User A", Cash = 10 },
                new UserPortfolio { ID = 2, Name = "User B", Cash = 0 }
            });

            _userPortfolioService = new UserPortfolioService(_mockRepository.Object);
        }

        [TestMethod]
        public void WhenGetAll_ShouldReturnAllUserPortfolio()
        {
            var result = _userPortfolioService.GetAll();

            var expectedShareCount = 2;
            Assert.AreEqual(expectedShareCount, result.Count);
        }

        [TestMethod]
        public void WhenGet_GivenValidUserPortfolioId_ShouldReturnThatUserPortfolio()
        {
            var portfolioId = _validPortfolioId;

            var result = _userPortfolioService.Get(portfolioId);

            var expectedShareId = portfolioId;
            Assert.AreEqual(expectedShareId, result.ID);

        }

        [TestMethod]
        [ExpectedException(typeof(UserPortfolioNotFoundException))]
        public void WhenGet_GivenNonExisitingUserPortfolioId_ShouldThrowUserPortfolioNotFoundException()
        {
            var shareId = 987654123;

            var result = _userPortfolioService.Get(shareId);
        }

        [TestMethod]
        public void WhenUpdate_GivenUpdatedUserProfolio_ShouldUpdateTheStateInRepository_And_ReturnTrue()
        {
            var portfolioId = _validPortfolioId;
            var userPortfolio = _userPortfolioService.Get(portfolioId);
            userPortfolio.Name = "udpate";
            userPortfolio.Cash = 10000000;
            userPortfolio.Shares[123] = 123;

            var result = _userPortfolioService.Update(userPortfolio);
            var updatedUserPortfolio = _userPortfolioService.Get(portfolioId);

            Assert.AreEqual(_validPortfolioId, updatedUserPortfolio.ID);
            Assert.AreEqual("udpate", updatedUserPortfolio.Name);
            Assert.AreEqual(10000000, updatedUserPortfolio.Cash);
            Assert.AreEqual(123, updatedUserPortfolio.Shares[123]);
            Assert.AreEqual(true, result);
        }
    }
}
