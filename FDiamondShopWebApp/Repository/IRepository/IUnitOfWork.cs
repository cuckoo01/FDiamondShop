﻿namespace FDiamondShop.API.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductVariantValueRepository ProductVariantValueRepository { get; }
        IUserRepository UserRepository { get; }
        IEmailRepository EmailRepository { get; }
        IDiscountRepository DiscountCodeRepository { get; }
        Task SaveAsync();
    }
}
