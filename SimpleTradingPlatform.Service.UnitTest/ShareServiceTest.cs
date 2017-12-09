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
    public class ShareServiceTest
    {
        private ShareService _shareService;
        private readonly Mock<IRepository> _mockRepository;
        private const int _validShareId = 1;

        public ShareServiceTest()
        {
            _mockRepository = new Mock<IRepository>();
            _mockRepository.Setup(m => m.Shares()).Returns(new List<Share> { 
                new Share { ID = _validShareId, Name = "Share A", Price = 10 },
                new Share { ID = 2, Name = "Share B", Price = 20 },
                new Share { ID = 3, Name = "Share C", Price = 30 }
            });

            _shareService = new ShareService(_mockRepository.Object);
        }

        [TestMethod]
        public void WhenGetAll_ShouldReturnAllShares()
        {
            var result = _shareService.GetAll();

            var expectedShareCount = 3;
            Assert.AreEqual(expectedShareCount, result.Count);
        }

        [TestMethod]
        public void WhenGet_GivenValidShareId_ShouldReturnThatShare()
        {
            var shareId = _validShareId;

            var result = _shareService.Get(shareId);

            var expectedShareId = shareId;
            Assert.AreEqual(expectedShareId, result.ID);

        }

        [TestMethod]
        [ExpectedException(typeof(ShareNotFoundException))]
        public void WhenGet_GivenNonExisitingShareId_ShouldThrowShareNotFoundException()
        {
            var shareId = 987654123;

            var result = _shareService.Get(shareId);
        }
    }
}
