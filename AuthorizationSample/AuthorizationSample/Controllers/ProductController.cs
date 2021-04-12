using System.Collections.Generic;
using AuthorizationSample.Constants;
using AuthorizationSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationSample.Controllers
{
    [ApiController]
    [Route("api/products")]

    public class ProductController : ControllerBase
    {
        [Authorize(Roles = Roles.AdministratorRole)]
        [HttpGet]
        public IActionResult Get()
        {
            var products = GetSampleProductList();

            return Ok(products);
        }

        private List<Product> GetSampleProductList()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Product1",
                    Price = 10
                },
                new Product
                {
                    Name = "Product2",
                    Price = 45
                }
            };
        }
    }
}
