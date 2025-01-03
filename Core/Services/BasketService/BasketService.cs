﻿using Core.DtoModels;
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
            var baskets = await _basketRepository.GetAllBasketItems(a => a.Id == id).FirstOrDefaultAsync();
            await _basketRepository.DeleteBasketItem(baskets);
            return true;
        }

        public async Task<bool> Pay(string mobile, string address, int userId)
        {
            var basket = await _basketRepository.GetAll(a => a.UserId == userId && a.Status == DataAccess.Enums.Status.Created)
                .FirstOrDefaultAsync();

            if (basket == null)
                return false;

            basket.Mobile = mobile;
            basket.Address = address;
            basket.Payed = DateTime.Now;
            basket.Status = DataAccess.Enums.Status.Final;

            await _basketRepository.Update(basket);

            return true;
        }

        public async Task<List<Basket>> GetUserOrders(int userId)
        {
            var baskets = await _basketRepository.GetAll(a => a.UserId == userId && a.Status != DataAccess.Enums.Status.Created)
                .Include(a => a.BasketItems)
                .ThenInclude(a => a.Book).AsNoTracking().ToListAsync();
            return baskets;
        }



        public async Task<List<AdminOrderDto>> GetAllOrders()
        {
            var baskets = await _basketRepository.GetAll(a => a.Status != DataAccess.Enums.Status.Created)
                .Include(a => a.User)
                .Include(a => a.BasketItems)
                .ThenInclude(a => a.Book)
                .Select(s => new AdminOrderDto()
                {
                    Address = s.Address,
                    Id = s.Id,
                    Mobile = s.Mobile,
                    Status = s.Status,
                    Payed = s.Payed,
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    Items = s.BasketItems.Select(c => c.Book.Title).ToList()
                })
                .AsNoTracking().ToListAsync();
            return baskets;
        }

        public async Task<AdminOrderDto> GetOrderWithId(int id)
        {
            var baskets = await _basketRepository.GetAll(a => a.Id == id)
                .Include(a => a.User)
                .Include(a => a.BasketItems)
                .ThenInclude(a => a.Book)
                .Select(s => new AdminOrderDto()
                {
                    Address = s.Address,
                    Id = s.Id,
                    Mobile = s.Mobile,
                    Status = s.Status,
                    Payed = s.Payed,

                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    Items = s.BasketItems.Select(c => c.Book.Title).ToList()
                })
                .AsNoTracking().FirstOrDefaultAsync();
            return baskets;
        }



        public async Task<bool> SetStatus(int Id, bool State)
        {
            var basket = await _basketRepository.GetAll(a => a.Id == Id).FirstOrDefaultAsync();
            if (State)
            {
                basket.Status = DataAccess.Enums.Status.Accepted;
            }
            else
            {
                basket.Status = DataAccess.Enums.Status.Rejected;

            }
            await _basketRepository.Update(basket);

            return true;
        }


    }
}
