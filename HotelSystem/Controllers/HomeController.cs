using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HotelSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Если не авторизован — перенаправляем на вход
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Identity/Account/Login");

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}