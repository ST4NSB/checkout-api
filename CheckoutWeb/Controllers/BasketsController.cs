using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLogic.Interfaces;
using Models.Requests;
using Models.Responses;

namespace CheckoutWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly ILogger<BasketsController> _logger;
        private readonly IBasketBLL _basketBLL;

        public BasketsController(ILogger<BasketsController> logger, IBasketBLL basketBLL)
        {
            _logger = logger;
            _basketBLL = basketBLL;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomerRequestModel customer)
        {
            var result = _basketBLL.CreateCustomer(customer);
            return StatusCode((int)result.Status, result.Response);
        }

        [HttpPut("{id}/article-line")]
        public ResponseModel<string> Put(int id, [FromBody] AddProductRequestModel product)
        {
            _basketBLL.AddProductToBasket(id, product);

            return new ResponseModel<string>();
        }

        [HttpGet("{id}")]
        public int Get(int id)
        {
            return id;
        }

        [HttpPatch("{id}")]
        public int Patch(int id)
        {
            return id;
        }
    }
}
