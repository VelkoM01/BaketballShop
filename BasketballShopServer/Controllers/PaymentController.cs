using BasketballShopServer.Repositories;
using BasketballShopSharedLibrary.Contracts;
using BasketballShopSharedLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasketballShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPayment paymentService, IProduct productService, IOrderRepository orderRepository) : ControllerBase
    {
        [HttpPost("checkout")]
        public async Task<ActionResult> CreateCheckoutSession([FromBody] CheckoutRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }

            var url = paymentService.CreateCheckoutSession(request.CartItems, request.DiscountCode);

            await productService.UpdateProductQuantitiesAsync(request.CartItems);

            var orders = request.CartItems.Select(cartItem => new Order
            {
                Name = cartItem.Name,
                Price = cartItem.Price,
                Quantity = cartItem.Quantity,
                Image = cartItem.Image
            }).ToList();

            await orderRepository.AddOrdersAsync(orders);

            return Ok(new { Url = url });
        }


    }
}
