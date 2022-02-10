using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Models.Requests;
using Models.Responses;

namespace CheckoutWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketBLL _basketBLL;

        public BasketsController(IBasketBLL basketBLL)
        {
            _basketBLL = basketBLL;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomerRequestModel customer)
        {
            var result = _basketBLL.CreateCustomer(customer);
            return FormatResult(result);
        }

        [HttpPut("{id}/article-line")]
        public IActionResult Put(int id, [FromBody] AddProductRequestModel product)
        {
            var result = _basketBLL.AddProductToBasket(id, product);
            return FormatResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _basketBLL.GetBasketDetails(id);
            return FormatResult(result);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProcessCustomerPaymentRequestModel basketProcessReq)
        {
            var result = _basketBLL.ProcessCustomerPayment(id, basketProcessReq);
            return FormatResult(result);
        }

        [NonAction]
        private IActionResult FormatResult<TResult>(ResponseModel<TResult> res)
        {
            if (!string.IsNullOrEmpty(res.ErrorMessage))
            {
                return StatusCode((int)res.Status, res.ErrorMessage);
            }

            return StatusCode((int)res.Status, res.Result);
        }
    }
}
