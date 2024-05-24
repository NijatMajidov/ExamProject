using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RocketMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        public IActionResult Index()
        {
            return View(_portfolioService.GetAllPortfolio());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            if(!ModelState.IsValid) 
            { 
                return View();
            }
            try
            {
                _portfolioService.CreatePortfolio(portfolio);
            }
            catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var exsist = _portfolioService.GetPortfolio(x => x.Id == id);
            if (exsist == null) return View("Error");
            return View(exsist);
        }
        [HttpPost]
        public IActionResult DeletePortfolio(int id)
        {
            var exsist = _portfolioService.GetPortfolio(x => x.Id == id);
            if (exsist == null) return View("Error");

            try
            {
                _portfolioService.DeletePortfolio(id);
            }
            catch (EntityNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var exsts = _portfolioService.GetPortfolio(x => x.Id == id);
            if (exsts == null) return View("Error");
            return View(exsts);
        }
        [HttpPost]
        public IActionResult Update(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _portfolioService.UpdatePortfolio(portfolio.Id, portfolio);
            }
            catch (EntityNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
