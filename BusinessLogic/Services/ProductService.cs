using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.VisualBasic;

namespace BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductDetailRepository _productDetailRepo;
        private readonly IProductImageRepository _productImageRepo;
        public ProductService(IProductRepository productRepository, IProductDetailRepository productDetailRepository, IProductImageRepository productImageRepository)
        {
            _productRepo = productRepository;
            _productDetailRepo = productDetailRepository;
            _productImageRepo = productImageRepository;
        }

        public async Task AddProduct(AddProductDTO addProductDTO)
        {
            var newProduct = new Product
            {
                ProductName = addProductDTO.ProductName,
                CategoryId = addProductDTO.CategoryId,
                Description = addProductDTO.Description,
                Thumbnail = addProductDTO.Thumbnail
            };

            await _productRepo.Add(newProduct);

            await _productDetailRepo.Add(new ProductDetail
            {
                ProductId = newProduct.ProductId,
                CakeSize = addProductDTO.CakeSize,
                Flavor = addProductDTO.Flavor,
                Price = addProductDTO.Price
            });

            foreach (var item in addProductDTO.Images)
            {
                await _productImageRepo.Add(new ProductImage
                {
                    ProductId = newProduct.ProductId,
                    ImageUrl = item
                });
            }
        }

        public async Task DeleteProduct(int productId)
        {
            await _productRepo.DeleteProduct(productId);
        }

        public async Task<IEnumerable<GetAdProductDTO>> GetAdProducts()
        {
            var products = await _productRepo.GetAllAdminProducts();
            List<GetAdProductDTO> adProducts = products.Select(item => new GetAdProductDTO
            {
                ProductId = item.ProductId,
                Name = item.ProductName,
                ImageUrl = item.Thumbnail,
                Price = item.ProductDetails.Min(p => p.Price),
                ShortDescription = item.Description,
                CategoryName = item.Category.CategoryName,
                CreatedAt = item.CreatedAt,
                CategoryId = item.CategoryId,
                Stock = item.ProductDetails.Min(p => p.Stock)
            }).ToList();
            return adProducts;
        }

        public async Task<IEnumerable<ProductListDTO>> GetByCategoryId(int id)
        {
            var products = await _productRepo.GetByCategory(id);
            List<ProductListDTO> productListDTOs = products.Select(item => new ProductListDTO
            {
                Id = item.ProductId,
                Name = item.ProductName,
                Price = item.ProductDetails.Min(p => p.Price),
                ImageUrl = item.Thumbnail
            }).ToList();
            return productListDTOs;
        }

        // public Task DeleteProduct(int productID)
        // {
        //     _productDetailRepo.Delete()
        // }

        public async Task<GetProductDetailDTO> GetProductDetail(int id)
        {
            // tư ma product detail -> ma product 
            var products = await _productRepo.GetProductById(id);
            var detail = products.ProductDetails.FirstOrDefault();

            return new GetProductDetailDTO
            {
                ProductId = detail.ProductId,
                Id = detail.ProductDetailId,
                Name = products.ProductName,
                Description = products.Description,
                Thumbnail = products.Thumbnail,
                CakeSize = detail.CakeSize,
                Flavor = detail.Flavor,
                Price = detail.Price,
                ImageUrls = products.ProductImages.Select(d => d.ImageUrl).ToList(),
                CategoryName = products.Category.CategoryName
            };
        }

        public async Task<IEnumerable<ProductListDTO>> GetProducts()
        {
            var products = await _productRepo.GetAllProducts();

            return products.Select(p => new ProductListDTO
            {
                Id = p.ProductId,
                Name = p.ProductName,
                Price = p.ProductDetails.Min(d => d.Price),
                ImageUrl = p.Thumbnail
            });
        }
    }
}