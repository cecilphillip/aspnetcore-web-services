using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsODataApiDemo;
using ProductsODataApiDemo.Data;
using ProductsODataApiDemo.Models;

//[assembly: ApiConventionType(typeof(DefaultApiConventions))]
[assembly: ApiConventionType(typeof(ProductODataApiConventions))]

namespace ProductsODataApiDemo.Controllers
{
    [ODataRoutePrefix("Products")]
    [ApiController]
    public class ProductsController : ODataController
    {
        private readonly DemoODataApiDbContext _ctx;
        
        public ProductsController(DemoODataApiDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("")]
        [EnableQuery]
        [ODataRoute]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _ctx.Products.ToListAsync();

            if (products.Count > 0)
                return Ok(products);

            return NotFound();
        }

        [HttpGet("")]
        [EnableQuery]
        [ODataRoute("({id})")]
        public async Task<ActionResult<Product>> GetProduct([FromODataUri]string id)
        {
            var product = await _ctx.Products.FindAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPatch("")]
        [ODataRoute("({id})")]
        public async Task<IActionResult> UpdateProduct([FromODataUri]string id, Delta<Product> delta)
        {
            var product = await _ctx.Products.FindAsync(id);

            if (product is null)
                return NotFound();

            delta.Patch(product);

            await _ctx.SaveChangesAsync();
            return Updated(product);
        }

        [HttpPost("")]
        [ODataRoute]
        public async Task<IActionResult> PostProduct([FromBody]Product newProduct)
        {
            _ctx.Products.Add(newProduct);
            await _ctx.SaveChangesAsync();
            return Created(newProduct);
        }

        [HttpDelete("")]
        [ODataRoute("({id})")]
        public async Task<ActionResult> DeleteProduct([FromODataUri]string id)
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
