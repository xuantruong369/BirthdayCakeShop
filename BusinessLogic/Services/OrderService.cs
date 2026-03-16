using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IProductDetailRepository _productDetailRepo;
        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, ICartItemRepository cartItemRepository, ICustomerRepository customerRepository, IProductDetailRepository productDetailRepository)
        {
            _orderRepo = orderRepository;
            _orderItemRepo = orderItemRepository;
            _cartItemRepo = cartItemRepository;
            _customerRepo = customerRepository;
            _productDetailRepo = productDetailRepository;
        }
        public async Task AddOrder(AddOrderDTO addOrderDTO, List<AddOrderItemDTO> addOrderItemDTOs)
        {
            decimal? sum  = 0;
            foreach(var item in addOrderItemDTOs)
            {
                sum += item.Quantity * item.UnitPrice;
            }

            var customer = await _customerRepo.GetByUserId(addOrderDTO.UserId);
            var order = new Order
            {
                CustomerId = customer.CustomerId,
                ActualAmount = sum,
                ShippingAddress = addOrderDTO.ShippingAddress,
                PaymentMethod = addOrderDTO.PaymentMethod,
                DeliveryDate = addOrderDTO.DeliveryDate,
                DeliveryTimeSlot = addOrderDTO.DeliveryTimeSlot,
                Note = addOrderDTO.Note
            };
            
            await _orderRepo.Add(order);

            foreach(var item in addOrderItemDTOs)
            {
                await _orderItemRepo.Add(new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductDetailId = item.ProductDetailId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.Quantity * item.UnitPrice
                });

                await _cartItemRepo.Delete(item.CartItemId);
            }
            
        }

        // public async Task<IEnumerable<OrderListItemDTO>> GetOrders()
        // {
        //     var orders = await _orderRepo.GetAllOrders();

        //     List<OrderListItemDTO> orderListItemDTOs = new List<OrderListItemDTO>();
        //     foreach(var order in orders)
        //     {
        //         var customer = await _customerRepo.GetCustomerById(order.CustomerId);
                
        //         List<OrderProductItemDTO> orderProductItemDTOs = new List<OrderProductItemDTO>();

        //         foreach (var productDetail in order.OrderItems)
        //         {
        //             var product = await _productDetailRepo.GetProductDetailById(productDetail.ProductDetailId);
        //             var productDetails = new OrderProductItemDTO
        //             {
        //                 ProductName = product.Product.ProductName,
        //                 ProductImage = product.Product.Thumbnail,
        //                 Quantity = productDetail.Quantity,
        //                 UnitPrice = productDetail.UnitPrice,
        //                 TotalPrice = productDetail.TotalPrice
        //             };
        //             orderProductItemDTOs.Add(productDetails);
        //         }

        //         var dto = new OrderListItemDTO
        //         {
        //             OrderId = order.OrderId,
        //             CustomerFullName = customer.CustomerName,
        //             Phone = customer.Phone,
        //             OrderDate = order.OrderDate,
        //             TotalAmount = order.TotalAmount,
        //             PaymentMethod = order.PaymentMethod,
        //             DeliveryAddress = order.ShippingAddress,
        //             DeliveryDate = order.DeliveryDate,
        //             DeliveryTimeSlot = order.DeliveryTimeSlot,
        //             OrderNote = order.Note,
        //             Products = orderProductItemDTOs
        //         };
        //         orderListItemDTOs.Add(dto);
        //     }
        //     return orderListItemDTOs;
        // }
        public async Task<IEnumerable<OrderListItemDTO>> GetOrders()
        {
            var orders = await _orderRepo.GetAllOrders();

            return orders.Select(order => new OrderListItemDTO
            {
                OrderId = order.OrderId,
                CustomerFullName = order.Customer.CustomerName,
                Phone = order.Customer.Phone,
                OrderDate = order.OrderDate,
                TotalAmount = order.ActualAmount,
                PaymentMethod = order.PaymentMethod,
                DeliveryAddress = order.ShippingAddress,
                DeliveryDate = order.DeliveryDate,
                DeliveryTimeSlot = order.DeliveryTimeSlot,
                OrderNote = order.Note,
                Products = order.OrderItems.Select(orderItem => new OrderProductItemDTO
                {
                    ProductName = orderItem.ProductDetail.Product.ProductName,
                    ProductImage = orderItem.ProductDetail.Product.Thumbnail,
                    Quantity = orderItem.Quantity,
                    UnitPrice = orderItem.UnitPrice,
                    TotalPrice = orderItem.TotalPrice
                }).ToList()
            }).ToList();
        }
    }
}