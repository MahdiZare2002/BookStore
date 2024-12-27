using BookStore.Models;
using Core.Services.BasketService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly BasketService _basketService;
        public OrderController(BasketService basketService)
        {
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket([FromBody] AddBasketDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Ok(new { res = false, msg = "شما لاگین نکرده اید" });
            }

            var result = await _basketService.AddToBasket(model.bookId, model.qty, Convert.ToInt32(userId));
            return Ok(new { res = true });
        }


        [Authorize]
        public async Task<IActionResult> Basket()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = await _basketService.GetUserBasket(Convert.ToInt32(userId));
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> RemoveBasket([FromBody] RemoveBasketDto model)
        {

            var res = await _basketService.RemoveItemBasket(model.Id);
            return Ok(new { res = true });
        }

        [Authorize]
        public async Task<IActionResult> Pay(PayDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = await _basketService.Pay(model.mobile, model.address, Convert.ToInt32(userId));
            return RedirectToAction("Index", "Profile");
        }
    }
}
