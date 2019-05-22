using System.Threading.Tasks;
using Products;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProductsGrpcDemo.Data;

namespace ProductsGrpcDemo
{
    public class GrpcProductService : ProductService.ProductServiceBase
    {
        private readonly GrpcDataContext _ctx;

        public GrpcProductService(GrpcDataContext ctx)
        {
            _ctx = ctx;
        }

        public override async Task<Product> GetProduct(ProductIdMessage request, ServerCallContext context)
        {
            var product = await _ctx.Products.FindAsync(request.Id);
            return product;
        }

        public override async Task<ProductList> GetProducts(GetProductsRequest request, ServerCallContext context)
        {
            var products = await _ctx.Products.ToListAsync();

            if (products.Count <= 0) return default;
            var pl = new ProductList();
            pl.Products.AddRange(products);
            return pl;
        }

        public override async Task<ProductOperationResponse> AddProduct(Product request, ServerCallContext context)
        {
            await _ctx.Products.AddAsync(request);
            return new ProductOperationResponse {OperationName = nameof(AddProduct), Success = true};
        }

        public override async Task<ProductOperationResponse> DeleteProduct(ProductIdMessage request,
            ServerCallContext context)
        {
            var product = await _ctx.Products.FindAsync(request.Id);
            if (product == null)
            {
                return new ProductOperationResponse {OperationName = nameof(DeleteProduct), Success = false};
            }

            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();

            return new ProductOperationResponse {OperationName = nameof(DeleteProduct), Success = true};
        }
    }
}