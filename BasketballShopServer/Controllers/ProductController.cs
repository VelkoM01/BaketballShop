using BasketballShopSharedLibrary.Contracts;
using BasketballShopSharedLibrary.Models;
using BasketballShopSharedLibrary.Response;
using Microsoft.AspNetCore.Mvc;

namespace BasketballShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productService;

        public ProductController(IProduct productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await productService.GetAllProducts(); return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddProduct(Product model)
        {
            if (model is null) return BadRequest("Model is Null");
            var response = await productService.AddProduct(model);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("update-quantities")]
        public async Task<IActionResult> UpdateQuantitiesAsync([FromBody] List<Order> orders)
        {
            try
            {
                await productService.UpdateProductQuantitiesAsync(orders);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
