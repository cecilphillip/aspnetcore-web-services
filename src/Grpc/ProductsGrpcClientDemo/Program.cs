using System;
using System.Threading.Tasks;
using Grpc.Core;
using Products;

namespace ProductsGrpcClientDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number here must match the port of the gRPC server
            var channel = new Channel("localhost:50051", ChannelCredentials.Insecure);
            var client = new ProductService.ProductServiceClient(channel);

            var response = await client.GetProductsAsync(new GetProductsRequest());

            foreach (var product in response.Products)
            {
                Console.WriteLine($"{product.Name} - {product.ProducedBy} - {product.Price}");
            }

            await channel.ShutdownAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
