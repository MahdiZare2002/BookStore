﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.BasketRepo
{
    public interface IBasketRepository
    {
        IQueryable<Basket> GetAll(Expression<Func<Basket, bool>> where = null);
        Task<Basket> GetById(int id);

        Task Add(Basket basket);
        Task Update(Basket basket);
        Task Delete(int id);
        Task Delete(Basket basket);

        IQueryable<BasketItems> GetAllBasketItems(Expression<Func<BasketItems, bool>> where = null);
        Task AddBasketItem(BasketItems item);
        Task DeleteBasketItem(BasketItems item);

    }
}
