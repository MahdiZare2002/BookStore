using DataAccess.Models;
using DataAccess.Repositories.BasketRepo;
using DataAccess.Repositories.BookRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.BasketService
{
    public class BasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBookRepository _bookRepository;
        public BasketService(IBasketRepository basketRepository, IBookRepository bookRepository)
        {
            _basketRepository = basketRepository;
            _bookRepository = bookRepository;
        }
        public async Task<bool> AddToBasket(int bookId, int Qty, int userId)
        {
            var basket = new Basket();
            basket = await _basketRepository.GetAll(a => a.UserId == userId && a.Status == DataAccess.Enums.Status.Created).FirstOrDefaultAsync();

            if (basket == null)
            {
                basket = new Basket()
                {
                    UserId = userId,
                    Created = DateTime.Now,
                    Status = DataAccess.Enums.Status.Created,
                    Address = "",
                    Mobile = "",
                    Payed = DateTime.Now,
                };
                await _basketRepository.Add(basket);
            }

            var book = await _bookRepository.GetById(bookId);

            if (book == null)
            {
                return false;
            }

            var bookItem = new BasketItems()
            {
                BasketId = basket.Id,
                Qty = Qty,
                BookId = book.Id,
                Created = DateTime.Now,
                Price = book.Price * Qty,
            };

            await _basketRepository.AddBasketItem(bookItem);

            return true;
        }

        public async Task<List<BasketItems>> GetUserBasket(int userId)
        {
            var baskets = await _basketRepository.GetAllBasketItems(a => a.Basket.UserId == userId
             && a.Basket.Status == DataAccess.Enums.Status.Created)
                .Include(a => a.Basket)
                .Include(a => a.Book).AsNoTracking().ToListAsync();
            return baskets;
        }

        public async Task<bool> RemoveItemBasket(int id)
        {
            var baskets = await _basketRepository.GetAllBasketItems(a => a.Id == Id).FirstOrDefaultAsync();
            await _basketRepository.DeleteBasketItem(baskets);
            return true;
        }
    }
}
