using CustomModelBinderSample.ModelBinders;
using CustomModelBinderSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBinderSample.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : Controller
    {
        [HttpGet]
        public IActionResult Get([ModelBinder(BinderType = typeof(PersonModelBinder))] Person person)
        {
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}
