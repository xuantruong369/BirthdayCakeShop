using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class Cart : Controller
    {
        private readonly ICartService _service;
        public Cart(ICartService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var car = await _service.GetOrCreateCart(userId);

            List<CartItemView> cartItemViews = car.CartItems.Select(item => new CartItemView
            {
                ProductId = item.ProductId,
                CartItemId = item.CartItemId,
                ProductDetailId = item.ProductDetailId,
                ProductName = item.ProductName,
                Thumbnail = item.Thumbnail,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList();
            
            return View(cartItemViews);
        }

        public async Task<IActionResult> AddCartItem(AddCartItem addCartItem)
        {
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var carItemDTO = new AddCartItemDTO
            {
                ProductDetailId = addCartItem.ProductDetailId,
                Quantity = addCartItem.Quantity
            };
            await _service.AddCartItem(userId, carItemDTO);
            return RedirectToAction("GetProductDetail","Product", new {maSp = addCartItem.ProductID});
            // tra ve mot product Id 
        }

        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            await _service.DeleteCartItem(cartItemId);
            return RedirectToAction("Index");
        }

    }
}