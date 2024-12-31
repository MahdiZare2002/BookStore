using Core.Services.BasketService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {

        private readonly BasketService _basketService;
        public ProfileController(BasketService basketservice)
        {
            _basketService = basketservice;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = await _basketService.GetUserOrders(Convert.ToInt32(userId));
            return View(data);
        }
    }
}
