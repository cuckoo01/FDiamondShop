﻿using FDiamondShop.API.Data;
using FDiamondShop.API.Models;
using FDiamondShop.API.Repository.IRepository;

namespace FDiamondShop.API.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly FDiamondContext _db;

        public OrderRepository(FDiamondContext db) : base(db)
        {
            _db = db;
        }

    }

}
