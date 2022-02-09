using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataAccess;
using BusinessLogic.Interfaces;

namespace CheckoutWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly ILogger<BasketsController> _logger;
        private IBasketBLL _basketBLL;

        public BasketsController(ILogger<BasketsController> logger, IBasketBLL basketBLL)
        {
            _logger = logger;
            _basketBLL = basketBLL;
        }

        [HttpPost]
        public int Post([FromBody] CreateCustomerRequest customer)
        {
            return _basketBLL.CreateCustomer(customer);
        }

        [HttpPut("{id}/article-line")]
        public string Put(int id, [FromBody] string value)
        {
            return value;
        }

        [HttpGet]
        public int Get(int id)
        {
            return id;
        }

        [HttpPatch]
        public int Patch(int id)
        {
            return id;
        }
    }
}
