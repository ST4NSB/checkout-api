using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace checkout.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly ILogger<BasketsController> _logger;

        public BasketsController(ILogger<BasketsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string Post([FromBody] string customer)
        {
            return customer;
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
