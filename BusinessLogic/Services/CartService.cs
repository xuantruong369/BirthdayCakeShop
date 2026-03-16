using System.Reflection;
using System.Security;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.VisualBasic;

namespace BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IProductDetailRepository _productDetailRepo;
        private readonly ICartItemRepository _cartItemRepo;

        public CartService(
            ICustomerRepository customerRepository,
            ICartRepository cartRepository,
            IProductDetailRepository productDetailRepository,
            ICartItemRepository cartItemRepository
        )
        {
            _customerRepo = customerRepository;
            _cartRepo = cartRepository;
            _productDetailRepo = productDetailRepository;
            _cartItemRepo = cartItemRepository;
        }
        public async Task AddCartItem(int? userId, AddCartItemDTO addCartItemDTO)
        {
           var customer = await _customerRepo.GetByUserId(userId);
            // 1. Kiểm tra và tạo giỏ hàng nếu chưa có
            if (customer.Cart == null)
            {
                var newCart = new Cart { CustomerId = customer.CustomerId };
                await _cartRepo.Add(newCart);
            // Đảm bảo đã lưu xuống DB
            }

            // 2. Lấy giỏ hàng kèm theo các Item (Dùng Include để tránh gọi DB nhiều lần)
            var cart = await _cartRepo.GetByCustomerId(customer.CustomerId);

            await _cartItemRepo.Add(
                new CartItem
                {
                    CartId = cart.CartId,
                    ProductDetailId = addCartItemDTO.ProductDetailId,
                    Quantity = addCartItemDTO.Quantity
                }
            );

            
        }

        public async Task DeleteCartItem(int cartItemId)
        {
            await _cartItemRepo.Delete(cartItemId);
        }

        // public async Task<CartDTO> GetOrCreateCart(int userId)
        // {
        //     var customer = await _customerRepo.GetByUserId(userId);
        //     if (customer.Cart == null)
        //     {
        //         await _cartRepo.Add(new Cart
        //         {
        //             CustomerId = customer.CustomerId
        //         });
        //     }

        //     var cart = await _cartRepo.GetByCustomerId(customer.CustomerId);

        //     List<int> cartItemIds = new List<int>();
        //     List<int?> quantitys = new List<int?>();
        //     List<int?> productDetailIds = new List<int?>();
        //     List<string?> productName = new List<string?>();
        //     List<string?> thumbnail = new List<string?>();
        //     List<decimal?> price = new List<decimal?>();

        //     cartItemIds = cart.CartItems.Select(p => p.CartItemId).ToList();
            
        //     foreach(var item in cart.CartItems)
        //     {
        //         quantitys.Add(item.Quantity);
        //         productDetailIds.Add(item.ProductDetailId);
        //     }

        //     foreach(var item in productDetailIds)
        //     {
        //         var productDetail  = await _productDetailRepo.GetProductDetailById(item);
        //         price.Add(productDetail.Price);
        //         productName.Add(productDetail.Product.ProductName);
        //         thumbnail.Add(productDetail.Product.Thumbnail);
        //     }

        //     List<CartItemDTO> cartItemDTOs = new List<CartItemDTO>();
        //     for (int i = 0; i < cartItemIds.Count; i++)
        //     {
        //         var item = new CartItemDTO
        //         {
        //             ProductDetailId = productDetailIds[i],
        //             ProductName = productName[i],
        //             Price = price[i],
        //             Thumbnail = thumbnail[i],
        //             Quantity = quantitys[i],
        //             CartItemId = cartItemIds[i]
        //             // CartItemId có thể để null hoặc gán nếu có list riêng
        //         };

        //         cartItemDTOs.Add(item);
        //     }
        //     return new CartDTO
        //     {
        //         CartId = cart.CartId,
        //         CartItems = cartItemDTOs
        //     };
        // }
        public async Task<CartDTO> GetOrCreateCart(int? userId)
        {
            var customer = await _customerRepo.GetByUserId(userId);
            
            // 1. Kiểm tra và tạo giỏ hàng nếu chưa có
            if (customer.Cart == null)
            {
                var newCart = new Cart { CustomerId = customer.CustomerId };
                await _cartRepo.Add(newCart);
            // Đảm bảo đã lưu xuống DB
            }

            // 2. Lấy giỏ hàng kèm theo các Item (Dùng Include để tránh gọi DB nhiều lần)
            var cart = await _cartRepo.GetByCustomerId(customer.CustomerId);
            
            // 3. Thay vì dùng 6 cái List, ta Map trực tiếp sang List<CartItemDTO>
            List<CartItemDTO> cartItemDTOs = new List<CartItemDTO>();

            foreach (var item in cart.CartItems)
            {
                // Lấy thông tin chi tiết sản phẩm (Nên Include Product trong Repo)
                var detail = await _productDetailRepo.GetProductDetailById(item.ProductDetailId);
                
                if (detail != null)
                {
                    cartItemDTOs.Add(new CartItemDTO
                    {
                        ProductId = detail.ProductId,
                        CartItemId = item.CartItemId,
                        ProductDetailId = item.ProductDetailId,
                        Quantity = item.Quantity,
                        Price = detail.Price,
                        ProductName = detail.Product?.ProductName ?? "N/A",
                        Thumbnail = detail.Product?.Thumbnail
                    });
                }
            }

            return new CartDTO
            {
                CartId = cart.CartId,
                CartItems = cartItemDTOs
            };
        }

    }
}