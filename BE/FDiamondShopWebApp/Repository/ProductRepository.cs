﻿using FDiamondShop.API.Data;
using FDiamondShop.API.Models;
using FDiamondShop.API.Repository.IRepository;
using FDiamondShop.API.Repository;
using FDiamondShop.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using FDiamondShop.API.Controllers;

namespace FDiamondShop.API.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly FDiamondContext _db;
        public ProductRepository(FDiamondContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetRecommendProducts(int productId)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            var recommendProducts = await _db.Products.Where(p => p.SubCategoryId == product.SubCategoryId && p.ProductId != productId && p.IsVisible == true).Include(p => p.ProductImages).ToListAsync();
            foreach (var products in recommendProducts)
            {
                products.ProductImages = products.ProductImages.Where(i => i.ImageUrl.Contains("bluenile")).Take(1).ToList();
            }
            return recommendProducts;
        }

        public async Task<List<Product>> SearchProductByName(string searchValue)
        {
            return await _db.Products.Include(p => p.ProductImages).
                Include(p => p.ProductVariantValues).
                ThenInclude(pv=>pv.Variant).
                Include(p => p.SubCategory).
                ThenInclude(o => o.Category).
                Where(p => p.ProductName.ToLower().Contains(searchValue.ToLower()) && p.IsVisible == true && p.IsDeleted == false).
                ToListAsync();
        }

        public async Task<Product> UpdateProduct(ProductUpdateDTO dto)
        {
            var product = await _db.Products.Include(p => p.ProductVariantValues).Include(p => p.ProductImages).FirstOrDefaultAsync(u => u.ProductId == dto.ProductId) ?? throw new Exception("Product Not Found!");
            return product ?? new Product();
        }
        
        }
        
    }

