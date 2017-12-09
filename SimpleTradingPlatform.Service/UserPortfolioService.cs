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
    public class UserPortfolioService : IUserPortfolioService
    {
        private readonly IRepository _repository;

        public UserPortfolioService(IRepository repository)
        {
            _repository = repository;
        }

        public List<UserPortfolio> GetAll()
        {
            return _repository.UserPortfolios().ToList();
        }

        public UserPortfolio Get(int id)
        {
            var userPortfolio = _repository.UserPortfolios().FirstOrDefault(s => s.ID == id);
            if (userPortfolio == null)
                throw new UserPortfolioNotFoundException();

            return userPortfolio;
        }

        public bool Update(UserPortfolio userPortfolio)
        {
            var update = _repository.UserPortfolios().FirstOrDefault(u => u.ID == userPortfolio.ID);
            if (update != null)
            {
                update = userPortfolio;
                return true;
            }

            return false;
        }
    }
}
