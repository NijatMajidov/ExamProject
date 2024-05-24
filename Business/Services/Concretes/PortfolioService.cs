using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Model;
using Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PortfolioService(IPortfolioRepository portfolioRepository,IWebHostEnvironment webHostEnvironment)
        {
            _portfolioRepository = portfolioRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public void CreatePortfolio(Portfolio portfolio)
        {
            if (portfolio == null) throw new EntityNullException("", "Portfolio Object null referance");
            if (portfolio.ImageFile == null) throw new Exceptions.FileNotFoundException("ImageFile", "Image file not found");
            if (!portfolio.ImageFile.ContentType.Contains("image/")) throw new FileContentTypeException("ImageFile", "Image File content type exception");
            if (portfolio.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "Image size exception");

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(portfolio.ImageFile.FileName);
            string path = _webHostEnvironment.WebRootPath + @"\uploads\portfolios\" + filename;

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                portfolio.ImageFile.CopyTo(stream);
            }

            portfolio.ImageUrl = filename;
            _portfolioRepository.Add(portfolio);
            _portfolioRepository.Commit();
        }

        public void DeletePortfolio(int id)
        {
            var exsits = _portfolioRepository.Get(x=> x.Id == id);
            if(exsits == null) throw new EntityNullException("", "Portfolio Object null referance");

            string path = _webHostEnvironment.WebRootPath + @"\uploads\portfolios\" + exsits.ImageUrl;
            if(!File.Exists(path)) throw new Exceptions.FileNotFoundException("ImageFile", "Image file not found");
            File.Delete(path);
            _portfolioRepository.Delete(exsits);
            _portfolioRepository.Commit();
        }

        public List<Portfolio> GetAllPortfolio(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.GetAll(func);
        }

        public Portfolio GetPortfolio(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.Get(func);
        }

        public void UpdatePortfolio(int id, Portfolio newPortfolio)
        {
            var exsits = _portfolioRepository.Get(x => x.Id == id);
            if (exsits == null) throw new EntityNullException("", "Portfolio Object null referance");

            if(newPortfolio == null) throw new EntityNullException("", "Portfolio Object null referance");
            if (newPortfolio.ImageFile != null)
            {
                if (!newPortfolio.ImageFile.ContentType.Contains("image/")) throw new FileContentTypeException("ImageFile", "Image File content type exception");
                if (newPortfolio.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "Image size exception");

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(newPortfolio.ImageFile.FileName);
                string path = _webHostEnvironment.WebRootPath + @"\uploads\portfolios\" + filename;

                string oldpath = _webHostEnvironment.WebRootPath + @"\uploads\portfolios\" + exsits.ImageUrl;
                if (!File.Exists(oldpath)) throw new Exceptions.FileNotFoundException("ImageFile", "Image file not found");
                File.Delete(oldpath);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    newPortfolio.ImageFile.CopyTo(stream);
                }

                exsits.ImageUrl = filename;
            }

            _portfolioRepository.Commit();
        }
    }
}
