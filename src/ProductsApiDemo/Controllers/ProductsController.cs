using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ProductsApiDemo;
using ProductsApiDemo.Data;
using ProductsApiDemo.Models;
using Microsoft.AspNetCore.Http;

[assembly: ApiConventionType(typeof(ProductApiConventions))]

namespace ProductsApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DemoApiDbContext _ctx;

        public LinkGenerator _linkGenerator { get; }

        public ProductsController(DemoApiDbContext ctx, LinkGenerator linkGenerator)
        {
            _ctx = ctx;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _ctx.Products.ToListAsync();

            if (products.Count > 0)
                return Ok(products);

            return NotFound();
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _ctx.Products.FindAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateProduct(string id, JsonPatchDocument<Product> productUpdates)
        {
            var product = await _ctx.Products.FindAsync(id);

            if (product is null)
                return NotFound();

            productUpdates.ApplyTo(product);

            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(Product newProduct)
        {
            _ctx.Products.Add(newProduct);
            await _ctx.SaveChangesAsync();
            var url = _linkGenerator.GetPathByAction(HttpContext, "GetProduct", "Products", new { id = newProduct.Id });
            return Created(url, newProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            var product = await _ctx.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
