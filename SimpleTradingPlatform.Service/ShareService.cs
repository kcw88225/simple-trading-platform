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
    public class ShareService : IShareService
    {
        private readonly IRepository _repository;

        public ShareService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Share> GetAll()
        {
            return _repository.Shares().ToList();
        }

        public Share Get(int id)
        {
            var share = _repository.Shares().FirstOrDefault(s => s.ID == id);
            if (share == null)
                throw new ShareNotFoundException();

            return share;
        }

    }
}
