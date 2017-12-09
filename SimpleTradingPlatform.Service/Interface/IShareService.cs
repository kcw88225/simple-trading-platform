using SimpleTradingPlatform.DataAccess.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradingPlatform.Service.Interface
{
    public interface IShareService
    {
        List<Share> GetAll();

        Share Get(int id);
    }
}
