using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestsSample.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _productContext;

        public ProductController(ProductContext productContext)
        {
            _productContext = productContext;
        }
        
        [HttpGet("{productCode}")]
        public IActionResult Get(string productCode)
        {
            var data = _productContext.Products.FirstOrDefault(x => x.Code == productCode);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _productContext.Products.ToList();

            return Ok(data);
        }
    }
}
