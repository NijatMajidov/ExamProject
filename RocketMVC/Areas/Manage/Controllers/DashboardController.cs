using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RocketMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin, Moderator")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
