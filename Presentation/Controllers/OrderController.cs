using System.Threading.Tasks;
using Azure.Core;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new CheckoutViewModel();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(List<OrderItemView> items) 
        {
            var model = new CheckoutViewModel {
                orderItemViews = items // Giữ lại danh sách từ Cart
            };
            return View(model); // Trả về View Checkout.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Xu ly luu don hang o day
            // add Order  can Customer ID
            var userId = HttpContext.Session.GetInt32("Id");
            var order = new AddOrderDTO
            {
                UserId = userId,
                PaymentMethod = model.PaymentMethod,
                ShippingAddress = model.DeliveryAddress,
                DeliveryDate = model.DeliveryDate,
                DeliveryTimeSlot = model.DeliveryTimeSlot,
                Note = model.OrderNote
            };
            // add Order Item ProductDetailId
            List<AddOrderItemDTO> orderItems = model.orderItemViews.Select(item => new AddOrderItemDTO
            {
                ProductDetailId = item.ProductDetailId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                CartItemId = item.CartItemId
            }).ToList();
            await _orderService.AddOrder(order, orderItems);
            // Delete Cart Item can Cartitem Id list
            
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Add()
        {
            return Ok();
        }
    }
}