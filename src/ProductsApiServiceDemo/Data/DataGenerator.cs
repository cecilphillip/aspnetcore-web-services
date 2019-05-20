using System.Collections.Generic;
using System.Globalization;
using Bogus;
using ProductsApiDemo.Models;

namespace ProductsApiServiceDemo.Data
{
    public static class DataGenerator
    {
        public static IEnumerable<Product> GetProducts(int count = 50)
        {

            Faker<Product> productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => f.Random.Guid().ToString("N"))
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(), CultureInfo.InvariantCulture))
                .RuleFor(p => p.Quantity, f => f.Random.Number(0, 200))
                .RuleFor(p => p.ProductedBy, f => f.Company.CompanyName());

            return productFaker.Generate(count);
        }
    }
}
