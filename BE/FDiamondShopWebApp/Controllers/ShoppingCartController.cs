﻿using AutoMapper;
using FDiamondShop.API.Data;
using FDiamondShop.API.Models;
using FDiamondShop.API.Models.DTO;

using FDiamondShop.API.Repository.IRepository;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FDiamondShop.API.Controllers
{
    [Route("api/ShoppingCart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly FDiamondContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ShoppingCartController(FDiamondContext db, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._response = new();
            _unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpPost("AddToCartLine")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> AddToCartLine([FromBody] CreateCartDTO createDTO)
        {
            var model = createDTO.CartLineItems;
            var user = _userManager.Users.First(u => u.UserName == createDTO.UserName);           

            var cartLine = new CartLine();
            
            cartLine.UserId = user.Id;

            await _unitOfWork.CartRepository.CreateAsync(cartLine);

            await _unitOfWork.SaveAsync();

            foreach (var item in model)
            {
                var product = await _unitOfWork.ProductRepository.GetAsync(p => p.ProductId == item.ProductId, includeProperties: "ProductImages,ProductVariantValues,SubCategory");
                if (product.Quantity == 0 || product.IsVisible == false)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Product is out of stock !");
                    return BadRequest(_response);
                }


                var cartLineItem = new CartLineItem
                {
                    CartLineId = cartLine.CartLineId,
                    ProductId = item.ProductId,
                    RingSize = item.RingSize,
                    Price = product.BasePrice,
                    Product = product
                };
                await _unitOfWork.CartRepository.CreateCartlineItem(cartLineItem);
                await _unitOfWork.SaveAsync();
            }
            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;

            return CreatedAtAction(nameof(GetAllCartLines), new { username = user.UserName }, _response);

        }
        [HttpPost("CheckDiamondExistInCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckDiamondExistInCart([FromBody] DiamondCart diamondCart)
        {
            Boolean isExist = false;
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == diamondCart.userName);

            var cartLines = await _unitOfWork.CartRepository.GetAllAsync(x=>x.CartLineItems.Count()==1,includeProperties: "CartLineItems");
            var cartLineItems= cartLines.SelectMany(x => x.CartLineItems).ToList();
            var product = cartLineItems.FirstOrDefault(x => x.ProductId == diamondCart.productId);

            
                    if (product != null)

                    {
                        isExist = true;
                        _response.IsSuccess = true;
                        _response.StatusCode = HttpStatusCode.OK;
                        _response.Result = isExist;
                        return Ok(_response);
                    }

            _response.Result = isExist;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        //[HttpPost("AddItemToCartLineItem")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> AddProductToCartLineItem(int cartLineId, [FromBody] CartLineItemCreateDTO model)
        //{
        //    var product = await _db.Products.FindAsync(model.ProductId);
        //    decimal price = product.BasePrice;
        //    var cartLineItem = new CartLineItem
        //    {
        //        CartLineId = cartLineId,
        //        ProductId = model.ProductId,
        //        RingSize = model.RingSize,
        //        Price = price
        //    };

        //    _db.CartLineItems.Add(cartLineItem);
        //    _response.IsSuccess = true;
        //    _response.StatusCode= HttpStatusCode.OK;
        //    await _db.SaveChangesAsync();

        //    return Ok(_response);
        //}
        [HttpGet("GetAllCartLines")]
        [ProducesResponseType(StatusCodes.Status200OK)]   
        public async Task<IActionResult> GetAllCartLines(string UserId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == UserId);
            var cartLines = await _unitOfWork.CartRepository.GetAllAsync(c => c.UserId == user.Id, includeProperties: "CartLineItems, CartLineItems.Product,CartLineItems.Product.ProductImages");
            if (cartLines.Count == 0)
            {
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.ErrorMessages.Add("EMPTY");
                return Ok(_response);
            }
            var cartLineDTOs = _mapper.Map<List<CartLineDTO>>(cartLines);
            
            foreach (var item in cartLineDTOs)
            {
                item.CartLineItems = _mapper.Map<List<CartLineItemDTO>>(cartLines
                    .Where(cl => cl.CartLineId == item.CartLineId)
                    .SelectMany(cl => cl.CartLineItems));
                foreach (var cli in item.CartLineItems)
                {
                    cli.Product = _mapper.Map<ProductDTO>(cartLines
                        .SelectMany(cl => cl.CartLineItems)
                        .Select(cli => cli.Product)
                        .FirstOrDefault());
                }
            }

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = cartLineDTOs;
            return Ok(_response);
        }
        //[HttpGet("GetAllCartLinesOrdered")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetAllCartLinesOrdered(string userName)
        //{
        //    var user = await _db.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        //    var cartLines = await _db.CartLines
        //                                  .Include(cl => cl.CartLineItems)
        //                                  .Where(cl => cl.UserId == user.Id && cl.IsOrdered == true)
        //                                  .ToListAsync();

        //    var cartLineDTOs = cartLines.Select(cl => new CartLineDTO
        //    {
        //        CartLineId = cl.CartLineId,

        //        CartLineItems = cl.CartLineItems.Select(cli => new CartLineItemDTO
        //        {
        //            ProductId = cli.ProductId,
        //            RingSize = cli.RingSize,
        //            Price = cli.Price
        //        }).ToList()
        //    }).ToList();
        //    if (cartLineDTOs == null)
        //    {
        //        _response.IsSuccess = false;
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.ErrorMessages.Add("Empty Cart here !");
        //        return NotFound();
        //    }
        //    _response.IsSuccess = true;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    _response.Result = cartLineDTOs;
        //    return Ok(_response);
        //}

        [HttpDelete("RemoveCartLine")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> RemoveCartLine(int cartLineId)
        {

            var cartLine = await _unitOfWork.CartRepository.FindCartlineId(cartLineId);
            var cartLineItems = await _unitOfWork.CartRepository.GetCartLineItems(cartLineId);
            await _unitOfWork.CartRepository.RemoveRange(cartLineItems);
            await _unitOfWork.CartRepository.RemoveAsync(cartLine);
            await _unitOfWork.SaveAsync();


            return NoContent();
        }
        [HttpDelete("RemoveCartLineItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveCartLineItem(int cartLineItemId)
        {

            var cartLineItems = await _unitOfWork.CartRepository.GetCartLineItems(cartLineItemId);
            var cartLine = await _unitOfWork.CartRepository.FindCartlineId(cartLineItems.First().CartLineId);
            await _unitOfWork.CartRepository.RemoveRange(cartLineItems);
            await _unitOfWork.SaveAsync();
            if (cartLine.CartLineItems.Count == 0)
            {
                await _unitOfWork.CartRepository.RemoveAsync(cartLine);
            }
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
