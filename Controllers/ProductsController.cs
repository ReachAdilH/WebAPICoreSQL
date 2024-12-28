using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICoreSQL.Data;
using WebAPICoreSQL.Model;

namespace WebAPICoreSQL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext appDbContext)
        {
            _context=appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var product = await _context.Product.ToListAsync();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        { 

            var product = await _context.Product.FirstOrDefaultAsync(x => x.Id == id);
            if(product==null)
            {
                return BadRequest("No record found");
            }
            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> GetProducts([FromBody]  Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            try {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch
            {

                return StatusCode(500, $"Internal Server error");
            }
          
        }
        

    }
}
