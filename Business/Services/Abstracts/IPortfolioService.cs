using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IPortfolioService
    {
        void CreatePortfolio(Portfolio portfolio);
        void DeletePortfolio(int id);
        void UpdatePortfolio(int id, Portfolio newPortfolio);
        Portfolio GetPortfolio(Func<Portfolio,bool>? func=null);
        List<Portfolio> GetAllPortfolio(Func<Portfolio, bool>? func = null);
    }
}
