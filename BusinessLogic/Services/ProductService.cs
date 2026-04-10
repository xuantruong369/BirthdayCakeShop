
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;


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
                // ProductId = item.ProductId,
                // Name = item.ProductName,
                // ImageUrl = item.Thumbnail,
                // Price = item.ProductDetails.Min(p => p.Price),
                // ShortDescription = item.Description,
                // CategoryName = item.Category.CategoryName,
                // CreatedAt = item.CreatedAt,
                // CategoryId = item.CategoryId,
                // Stock = item.ProductDetails.Min(p => p.Stock)
                ProductId = item.ProductId,
                Name = item.ProductName,
                ImageUrl = item.Thumbnail,
                Price = item.ProductDetails.FirstOrDefault().Price,
                ShortDescription = item.Description,
                CategoryName = item.Category.CategoryName,
                CreatedAt = item.CreatedAt,
                CategoryId = item.CategoryId,
                Stock = item.ProductDetails.FirstOrDefault().Stock
            }).ToList();
            return adProducts;
        }

        public async Task<AdminProductDTO> GetAllProductById(int productId)
        {
            var product = await _productRepo.GetProductById(productId);
            List<string?> imagesProductDetail = product.ProductImages.Select(item => item.ImageUrl).ToList();

            return new AdminProductDTO
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                Description = product.Description,
                Thumbnail = product.Thumbnail,
                Images = imagesProductDetail,
                Price = product.ProductDetails.FirstOrDefault().Price,
                CakeSize = product.ProductDetails.FirstOrDefault().CakeSize,
                Flavor = product.ProductDetails.FirstOrDefault().Flavor
            };
        }

        public async Task<IEnumerable<ProductListDTO>> GetByCategoryId(int id)
        {
            var products = await _productRepo.GetByCategory(id);
            List<ProductListDTO> productListDTOs = products.Select(item => new ProductListDTO
            {
                // Id = item.ProductId,
                // Name = item.ProductName,
                // Price = item.ProductDetails.Min(p => p.Price),
                // ImageUrl = item.Thumbnail
                Id = item.ProductId,
                Name = item.ProductName,
                Price = item.ProductDetails.FirstOrDefault().Price,
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
                // Id = p.ProductId,
                // Name = p.ProductName,
                // Price = p.ProductDetails.Min(d => d.Price),
                // ImageUrl = p.Thumbnail
                Id = p.ProductId,
                Name = p.ProductName,
                Price = p.ProductDetails.FirstOrDefault().Price,
                ImageUrl = p.Thumbnail
            });
        }

        public async Task UpdateAllProduct(AdminProductDTO edit)
        {
            var product = await _productRepo.GetProductById(edit.ProductId);
            if (product != null)
            {
                product.ProductName = edit.ProductName;
                product.Description = edit.Description;
                product.Thumbnail = edit.Thumbnail;
                product.CategoryId = edit.CategoryId;

                await _productRepo.Update(product);
            }

            var productDetail = await _productDetailRepo.GetHeadProductDetailByProductId(edit.ProductId);
            if (productDetail != null)
            {
                productDetail.Price = edit.Price;
                productDetail.CakeSize = edit.CakeSize;
                productDetail.Flavor = edit.Flavor;
                await _productDetailRepo.Update(productDetail);
            }

            var productImages = await _productImageRepo.GetByProduct(edit.ProductId);
            if (productImages != null)
            {
                foreach (var item in productImages)
                {
                    await _productImageRepo.Delete(item.ImageId);
                }
            }
            if (edit.Images != null)
            {
                foreach (var item in edit.Images)
                {
                    await _productImageRepo.Add(new ProductImage
                    {
                        ProductId = edit.ProductId,
                        ImageUrl = item
                    });
                }
            }

        }

        public async Task<(IEnumerable<GetAdProductDTO> items, int total)> SearchAdProducts(string search, int? categoryId, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 10)
        {
            var products = await _productRepo.GetAllAdminProducts();

            // Áp dụng filter theo search keyword
            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(search.ToLower()) ||
                                               p.Description.ToLower().Contains(search.ToLower())).ToList();
            }

            // Filter theo danh mục
            if (categoryId.HasValue && categoryId > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId).ToList();
            }

            // Filter theo giá
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.ProductDetails.FirstOrDefault()?.Price >= minPrice).ToList();
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.ProductDetails.FirstOrDefault()?.Price <= maxPrice).ToList();
            }

            // Tổng số sản phẩm phù hợp
            int totalCount = products.Count();

            // Phân trang
            var pagedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Map sang DTO
            List<GetAdProductDTO> adProducts = pagedProducts.Select(item => new GetAdProductDTO
            {
                ProductId = item.ProductId,
                Name = item.ProductName,
                ImageUrl = item.Thumbnail,
                Price = item.ProductDetails.FirstOrDefault()?.Price ?? 0,
                ShortDescription = item.Description,
                CategoryName = item.Category?.CategoryName,
                CreatedAt = item.CreatedAt,
                CategoryId = item.CategoryId,
                Stock = item.ProductDetails.FirstOrDefault()?.Stock ?? 0
            }).ToList();

            return (adProducts, totalCount);
        }
    }
}